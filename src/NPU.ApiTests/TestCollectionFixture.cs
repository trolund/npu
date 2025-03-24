using NPU.Data.Base;

namespace NPU.ApiTests;

public class TestCollectionFixture(ICosmosDbService cosmosDbService): IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        Console.WriteLine("🏁 Running test collection setup.");
    }

    public async Task DisposeAsync()
    {
        await cosmosDbService.RemoveDbSetupAsync();
    }
}