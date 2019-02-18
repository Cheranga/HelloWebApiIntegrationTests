using System;

namespace Api.DAL
{
    public class CustomerReadDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
    }
}