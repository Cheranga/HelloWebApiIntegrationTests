using System.Linq;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerService.GetCustomersAsync();

            if (customers == null || !customers.Any())
            {
                return new EmptyResult();
            }

            return Ok(customers);
        }
    }
}