using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DAL
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<CustomerReadDataModel> _customers;

        public CustomerRepository()
        {
            _customers = new List<CustomerReadDataModel>
            {
                new CustomerReadDataModel {Id = 1, Name = "Cheranga", Address = "Melbourne"},
                new CustomerReadDataModel {Id = 1, Name = "Tom", Address = "Sydney"},
                new CustomerReadDataModel {Id = 1, Name = "Jenna", Address = "Brisbane"}
            };
        }

        public Task<List<CustomerReadDataModel>> GetAllAsync()
        {
            return Task.FromResult(_customers.AsReadOnly().ToList());
        }
    }
}