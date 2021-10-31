using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hf.Core.EfCore.GenericRepoitory;
using Hf.Core.EfCore.Models;
using Microsoft.EntityFrameworkCore;


namespace Hf.Core.EfCore.Services
{
    public class CustomerService
    {
        private readonly IRepository _repository;

        public CustomerService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> GetPaginatedListAsync()
        {
            Filter<Customer> filter = new Filter<Customer>();
            filter.Conditions.Add(e => e.FName.Contains("Ha"));
            filter.Includes = q => q.Include(e => e.Addresses);
            filter.OrderBy = q => q.OrderBy(e => e.NationalCode);
            filter.Skip = 0;
            filter.Take = 4;

            long count = await _repository.GetLongCountAsync(filter.Conditions);

            return await _repository.GetListAsync(filter);
        }
    }
}
