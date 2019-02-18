using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTO;

namespace Api.Services
{
    public interface ICustomerService
    {
        Task<List<DisplayCustomerDto>> GetCustomersAsync();
    }
}