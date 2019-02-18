using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.DAL
{
    public interface ICustomerRepository
    {
        Task<List<CustomerReadDataModel>> GetAllAsync();
    }
}