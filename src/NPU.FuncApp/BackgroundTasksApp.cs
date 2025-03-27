using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using NPU.FuncApp;

[assembly: FunctionsStartup(typeof(BackgroundTasksApp))]

namespace NPU.FuncApp
{
    public class BackgroundTasksApp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var cosmosConnectionString = Environment.GetEnvironmentVariable("CosmosDBConnection");

            builder.Services.AddSingleton<CosmosClient>(s => new CosmosClient(cosmosConnectionString));
        }
    }
}