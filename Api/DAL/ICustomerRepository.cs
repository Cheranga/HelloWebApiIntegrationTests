using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.DAL
{
    public interface ICustomerRepository
    {
        Task<List<CustomerReadDataModel>> GetAllAsync();
        Task<bool> CreateCustomerAsync(CustomerWriteModel customer);
    }

    public class CustomerWriteModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
    }
}