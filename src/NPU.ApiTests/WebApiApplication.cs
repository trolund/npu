using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NPU.Data.Base;
using NPU.Data.Config;

namespace NPU.ApiTests;

public class WebApiApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<CosmosSettings>();
            services.AddSingleton(new CosmosSettings()
            {
                DB_CONNECTION_STRING =
                    "AccountEndpoint=http://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                DB_NAME = "NPUTestDB",
                CON_NAME = "data"
            });
            
            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to resolve dependencies
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
        
            var cosmosDbService = scopedServices.GetRequiredService<ICosmosDbService>();

            // Ensure database is created
            cosmosDbService.EnsureDbSetupAsync().GetAwaiter();

            // Seed the database
            DatabaseSeeding.SeedDatabase(cosmosDbService).GetAwaiter();
        });
        
    }
}