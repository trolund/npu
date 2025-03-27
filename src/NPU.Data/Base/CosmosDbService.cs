using Microsoft.Azure.Cosmos;
using NPU.Data.Config;

namespace NPU.Data.Base;

public class CosmosDbService(CosmosSettings config) : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient = config.DB_CONNECTION_STRING != null ? new CosmosClient(config.DB_CONNECTION_STRING,
        new CosmosClientOptions
        {
            ApplicationName = config.DB_NAME,
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true
        }) : throw new ArgumentException("Connection string is required");

    private readonly string _databaseName = config.DB_NAME;
    private readonly string _containerName = config.CON_NAME;
    
    public async Task EnsureDbSetupAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/partitionKey");
    }
    
    public async Task RemoveDbSetupAsync()
    {
        var database = _cosmosClient.GetDatabase(_databaseName);
        await database.DeleteAsync();
    }

    public async Task<Container> GetContainerAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        return await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/partitionKey");
    }
}