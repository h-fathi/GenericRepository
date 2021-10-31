using Hf.Core.EfCore.GenericRepoitory;
using Hf.Core.EfCore.Models;
using Hf.Core.EfCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IRepository> mockRepository = new Mock<IRepository>();

        private CustomerService customerService => new CustomerService(mockRepository.Object);

        [Fact]
        public async Task GetPaginatedListAsync_WithNoParams_ReturnsListOfCustomers()
        {
            // Arrange
            List<Customer> fakeCustomerList = new List<Customer>()
            {
                new Customer { FName = "Hossein", LName = "Fathi" }
            };

            mockRepository.Setup(mr => mr.GetListAsync(
                It.IsAny<Filter<Customer>>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(fakeCustomerList);

            // Act
            List<Customer> employees = await customerService.GetPaginatedListAsync();

            // Assert
            Assert.Single(employees);
        }
    }
}
