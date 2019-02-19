using System;
using System.Diagnostics;
using Api.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Integration.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //
            // Setup the tests related implementations
            //
            //builder.ConfigureTestServices(services => { services.AddSingleton<ICustomerRepository, TestCustomerRepository>(); });
        }
    }
}
