using NPU.Bl;
using NPU.Data.Base;
using NPU.Data.Repositories;

namespace NPU;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(CosmosRepository<>));
        services.AddScoped<NoteRepository>();
        services.AddScoped<NotesService>();
        

        return services;
    }
}