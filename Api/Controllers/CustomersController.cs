using System.Linq;
using System.Net;
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

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerDto customer)
        {
            //
            // TODO: Perform validation
            //
            var status = await _customerService.CreateCustomerAsync(customer);
            if (status)
            {
                return Ok();
            }

            return StatusCode((int) (HttpStatusCode.InternalServerError));
        }
    }

    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}