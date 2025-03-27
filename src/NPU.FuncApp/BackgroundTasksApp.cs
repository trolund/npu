using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using NPU.FuncApp;

[assembly: FunctionsStartup(typeof(BackgroundTasksApp))]

namespace NPU.FuncApp
{
    public class BackgroundTasksApp : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.development.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    
        public override void Configure(IFunctionsHostBuilder builder)
        {
        }
    }
}