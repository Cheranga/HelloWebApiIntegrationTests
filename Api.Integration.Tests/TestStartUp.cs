using Api.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Integration.Tests
{
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
            
        }

        protected override void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, TestCustomerRepository>();
        }
    }
}