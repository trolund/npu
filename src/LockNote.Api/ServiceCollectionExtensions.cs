using LockNote.Bl;
using LockNote.Data;
using LockNote.Data.Base;
using LockNote.Data.Repositories;

namespace LockNote;

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