using NPU.Bl;
using NPU.Data.Base;
using NPU.Data.DataHandlers;
using NPU.Data.Model;
using NPU.Data.Repositories;

namespace NPU;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
        
        // Repositories
        services.AddScoped<NpuRepository>();
        services.AddScoped<ScoreRepository>();
        
        // Services
        services.AddScoped<NpuService>();
        
        // Data Handlers
        services.AddScoped<IBlobStorageDriver, FileStorageDriver>();
        
        return services;
    }
}