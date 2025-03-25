using NPU.Data.Base;

namespace NPU.ApiTests;

public class TestCollectionFixture(ICosmosDbService cosmosDbService): IAsyncLifetime
{
    public Task InitializeAsync()
    {
        Console.WriteLine("🏁 Running test collection setup.");
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await cosmosDbService.RemoveDbSetupAsync();
    }
}