using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DAL;
using Api.DTO;
using Api.Util;

namespace Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<DisplayCustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            var displayCustomers = customers.ToDisplay();

            return displayCustomers;
        }
    }
}