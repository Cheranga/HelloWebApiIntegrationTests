using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DAL;

namespace Api.Integration.Tests
{
    public class TestCustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerWriteModel> _customers;

        public TestCustomerRepository()
        {
            _customers = new List<CustomerWriteModel>
            {
                new CustomerWriteModel { Name = "Test Cheranga", Address = "Melbourne"},
                new CustomerWriteModel { Name = "Test Tom", Address = "Sydney"},
                new CustomerWriteModel { Name = "Test Jenna", Address = "Brisbane"}
            };
        }

        public Task<List<CustomerReadDataModel>> GetAllAsync()
        {
            var customers = _customers.Select(x => new CustomerReadDataModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return Task.FromResult(customers);
        }

        public Task<bool> CreateCustomerAsync(CustomerWriteModel customer)
        {
            if (customer == null)
            {
                return Task.FromResult(false);
            }

            _customers.Add(customer);

            return Task.FromResult(true);
        }
    }
}