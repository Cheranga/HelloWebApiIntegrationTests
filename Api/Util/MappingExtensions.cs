using System.Collections.Generic;
using System.Linq;
using Api.DAL;
using Api.DTO;

namespace Api.Util
{
    public static class MappingExtensions
    {
        public static List<DisplayCustomerDto> ToDisplay(this IEnumerable<CustomerReadDataModel> customers)
        {
            var customerList = customers?.ToList() ?? new List<CustomerReadDataModel>();
            if (!customerList.Any())
            {
                return new List<DisplayCustomerDto>();
            }

            var customersToDisplay = customerList.Select(x => new DisplayCustomerDto
            {
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return customersToDisplay;
        }
    }
}