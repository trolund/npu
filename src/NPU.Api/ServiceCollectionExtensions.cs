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
        services.AddSingleton<ICosmosDbService, CosmosDbService>();
        services.AddScoped<NpuService>();
        services.AddScoped<FileUploadService>();
        
        // Data Handlers
        services.AddScoped<IBlobStorageDriver, BlobStorageDriver>();
        
        return services;
    }
}