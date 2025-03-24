using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NPU.Infrastructure.Config;
using Xunit.Abstractions;

namespace NPU.ApiTests;

public class WebApiApplication : WebApplicationFactory<Program>
{

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTesting");

        builder.ConfigureHostConfiguration(p =>
        {
                
        });
        
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(new CosmosSettings()
            {
                DB_CONNECTION_STRING = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=",
                DB_NAME = "NPUTestDB",
                CON_NAME = "data"
            });
        });

        return base.CreateHost(builder);
    }
}