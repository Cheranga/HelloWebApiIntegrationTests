using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Api.DAL;
using Api.DTO;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Tests.Customers
{
    public class CustomersIntegrationTestsWithWebApplicationFactory : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public CustomersIntegrationTestsWithWebApplicationFactory(TestWebApplicationFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _httpClient = factory.CreateClient();
        }

        private readonly HttpClient _httpClient;

        [Fact]
        public async Task CreateCustomerTest()
        {
            //
            // Arrange
            //
            var tempClient = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services => { services.AddSingleton<ICustomerRepository, TestCustomerRepository>(); });
                })
                .CreateClient();
            //
            // Act
            //
            var getCustomershttpResponse = await tempClient.GetAsync("/api/customers");
            //
            // Assert
            //
            getCustomershttpResponse.EnsureSuccessStatusCode();

            var displayCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var currentCustomerCount = displayCustomers.Count;

            var createCustomerHttpResponse = await tempClient.PostAsync(@"/api/customers", new StringContent(
                JsonConvert.SerializeObject(new CreateCustomerDto
                {
                    Name = "blah name",
                    Address = "blah address"
                }), Encoding.UTF8, "application/json"));

            createCustomerHttpResponse.EnsureSuccessStatusCode();

            getCustomershttpResponse = await tempClient.GetAsync(@"/api/customers");
            getCustomershttpResponse.EnsureSuccessStatusCode();
            var updatedCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var updatedCustomerCount = updatedCustomers.Count;

            Assert.True(updatedCustomerCount > currentCustomerCount);
        }

        [Fact]
        public async Task GetAllCustomersWithTestSpecificData()
        {
            //
            // Arrange
            //
            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<CustomerReadDataModel>
            {
                new CustomerReadDataModel {Id = "some id 1", Name = "some name 1", Address = "some address 1"},
                new CustomerReadDataModel {Id = "some id 1", Name = "some name 2", Address = "some address 2"},
                new CustomerReadDataModel {Id = "some id 1", Name = "some name 3", Address = "some address 3"}
            });

            var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services => { services.AddSingleton(customerRepository.Object); });
                })
                .CreateClient();

            

            var getAllCustomersResponse = await client.GetAsync(@"/api/customers");
            getAllCustomersResponse.EnsureSuccessStatusCode();

            var allCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getAllCustomersResponse.Content.ReadAsStringAsync());
            Assert.True(allCustomers.Any());
        }


        [Fact]
        public async Task GetAllCustomersTest()
        {
            //
            // Act
            //
            var httpResponse = await _httpClient.GetAsync("/api/customers");
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();

            var displayCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await httpResponse.Content.ReadAsStringAsync());

            Assert.NotNull(displayCustomers);
        }
    }
}