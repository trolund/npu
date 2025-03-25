using NPU.Bl;
using NPU.Data.Base;
using NPU.Data.DataHandlers;
using NPU.Data.Repositories;

namespace NPU;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
        
        // Repositories
        services.AddScoped<INpuRepository, NpuRepository>();
        services.AddScoped<IScoreRepository, ScoreRepository>();
        
        // Services
        services.AddSingleton<ICosmosDbService, CosmosDbService>();
        services.AddScoped<INpuService, NpuService>();
        services.AddScoped<IFileUploadService, FileUploadService>();
        
        // Data Handlers
        services.AddScoped<IBlobStorageDriver, BlobStorageDriver>();
        
        return services;
    }
}