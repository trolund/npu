using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.ApiTests;

public static class DatabaseSeeding
{
    public static readonly List<Npu> Npus = [
        new Npu
        {
            Id = Guid.NewGuid().ToString(),
            Name = "a",
            Description = "a",
            Images = ["image1.gif", "image2.jpg"]
        },
        new Npu
        {
            Id = Guid.NewGuid().ToString(),
            Name = "b",
            Description = "a",
            Images = ["image1.gif", "image2.jpg", "image2.jpg"]
        },
        new Npu
        {
            Id = Guid.NewGuid().ToString(),
            Name = "c",
            Description = "a",
            Images = ["image1.gif", "image2.jpg", "image3.jpg", "image4.jpg"]
        },
        new Npu
        {
            Id = Guid.NewGuid().ToString(),
            Name = "d",
            Description = "a",
            Images = ["image1.gif", "image2.pdf"]
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