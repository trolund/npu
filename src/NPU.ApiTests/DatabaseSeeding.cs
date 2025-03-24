using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.ApiTests;

public static class DatabaseSeeding
{
    public static readonly List<Npu> Npus = [
        new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "name",
            Description = "description",
            Images = ["image1.gif", "image2.jpg"]
        }
    ];

    public static async Task SeedDatabase(ICosmosDbService cosmosDbService)
    {
        var npuRepository = new CosmosRepository<Npu>(cosmosDbService);

        foreach (var npu in Npus)
        {
            await npuRepository.AddAsync(npu);
        }
    }
}