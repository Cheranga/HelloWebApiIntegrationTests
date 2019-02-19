using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<CustomerWriteModel> _customers;

        public CustomerRepository()
        {
            _customers = new List<CustomerWriteModel>
            {
                new CustomerWriteModel {Name = "Cheranga", Address = "Melbourne"},
                new CustomerWriteModel {Name = "Tom", Address = "Sydney"},
                new CustomerWriteModel {Name = "Jenna", Address = "Brisbane"}
            };
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
    }
}