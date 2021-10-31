using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Hf.Core.EfCore.GenericRepoitory;
using Hf.Core.EfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hf.EfCore.GenericRepository.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;

        public CustomerController(
            IRepository repository)
        {
            _repository = repository;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {

            List<string> search = new List<string>() { "Hossein", "Fathi" };
            string sqlQuery = "Select * from Customer Where FName LIKE @p0 + '%' and LName LIKE @p1 + '%'";
            List<Customer> items = await _repository .GetFromRawSqlAsync<Customer>(sqlQuery, search);

            var filter = new PaginationFilter<Customer>();
            filter.Conditions.Add(e => e.FName.Contains("Ho"));
            filter.Conditions.Add(e => e.LName.Contains("Fa"));
            filter.PageIndex = 1;
            filter.PageSize = 10;
            PaginatedList<Customer> paginatedList = await _repository.GetListAsync(filter);


            List<Customer> lists = await _repository.GetQueryable<Customer>().ToListAsync();
            return Ok(lists);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = await _repository.GetByIdAsync<Customer>(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                try
                {
                    //it's just a sample :)

                    object[] primaryKeys = await _repository.InsertAsync(customer);
                    long customerId = (long)primaryKeys[0];

                    customer.Addresses.ToList().ForEach(async item =>
                    {
                        Address customerAddress = new Address()
                        {
                            CustomerId = customerId,
                            AddressName = item.AddressName,
                            IsActive = item.IsActive
                        };

                        await _repository.InsertAsync(customerAddress);

                    });
                    
                    // commit
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }

                return RedirectToAction(nameof(Index));

            }
            return Ok(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _repository.GetByIdAsync<Customer>(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Customer customerToBeUpdated = await _repository.GetByIdAsync<Customer>(customer.Id);
                customerToBeUpdated.FName = customer.FName;
                customerToBeUpdated.LName = customer.LName;
                await _repository.UpdateAsync(customerToBeUpdated);
                return Ok(customer);
            }
            return BadRequest(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _repository.GetByIdAsync<Customer>(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            Customer customer = await _repository.GetByIdAsync<Customer>(id);
            await _repository.DeleteAsync(customer);
            return Ok(customer);
        }

    }
}
