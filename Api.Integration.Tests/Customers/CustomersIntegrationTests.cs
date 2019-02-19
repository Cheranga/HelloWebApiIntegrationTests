using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Api.DAL;
using Api.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Tests.Customers
{
    public class CustomersIntegrationTests
    {   
        private readonly HttpClient _httpClient;

        public CustomersIntegrationTests()
        {
            //
            // Create the test server
            //
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICustomerRepository, TestCustomerRepository>();
                }));
            //
            // Get the httpclient instance from the server
            //
            _httpClient = testServer.CreateClient();
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

        [Fact]
        public async Task CreateCustomerTest()
        {
            //
            // Act
            //
            var getCustomershttpResponse = await _httpClient.GetAsync("/api/customers");
            //
            // Assert
            //
            getCustomershttpResponse.EnsureSuccessStatusCode();

            var displayCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var currentCustomerCount = displayCustomers.Count;

            var createCustomerHttpResponse = await _httpClient.PostAsync(@"/api/customers", new StringContent(
                JsonConvert.SerializeObject(new CreateCustomerDto
                {
                    Name = "blah name",
                    Address = "blah address"
                }), Encoding.UTF8, "application/json"));

            createCustomerHttpResponse.EnsureSuccessStatusCode();

            getCustomershttpResponse = await _httpClient.GetAsync(@"/api/customers");
            getCustomershttpResponse.EnsureSuccessStatusCode();
            var updatedCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var updatedCustomerCount = updatedCustomers.Count;

            Assert.True(updatedCustomerCount > currentCustomerCount);

        }

        [Fact]
        public async Task CreateCustomerTest2()
        {
            var getCustomershttpResponse = await _httpClient.GetAsync("/api/customers");

            // Must be successful.
            getCustomershttpResponse.EnsureSuccessStatusCode();

            var displayCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var currentCustomerCount = displayCustomers.Count;

            var createCustomerHttpResponse = await _httpClient.PostAsync(@"/api/customers", new StringContent(
                JsonConvert.SerializeObject(new CreateCustomerDto
                {
                    Name = "blah name",
                    Address = "blah address"
                }), Encoding.UTF8, "application/json"));

            createCustomerHttpResponse.EnsureSuccessStatusCode();

            getCustomershttpResponse = await _httpClient.GetAsync(@"/api/customers");
            getCustomershttpResponse.EnsureSuccessStatusCode();
            var updatedCustomers = JsonConvert.DeserializeObject<List<DisplayCustomerDto>>(await getCustomershttpResponse.Content.ReadAsStringAsync());

            var updatedCustomerCount = updatedCustomers.Count;

            Assert.True(updatedCustomerCount > currentCustomerCount);

        }
    }
}