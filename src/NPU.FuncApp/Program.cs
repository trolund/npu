using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NPU.Bl;
using NPU.Data.Base;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// set up services
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
builder.Services.AddScoped<NpuService>();

builder.Build().Run();