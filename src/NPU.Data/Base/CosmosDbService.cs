using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Base;

public class CosmosDbService(string? connectionString, CosmosDbSettings settings) : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient = connectionString != null ? new CosmosClient(connectionString,
        new CosmosClientOptions
        {
            ApplicationName = "LockNote",
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true
        }) : throw new ArgumentException("Connection string is required");

    private readonly string _databaseName = settings.DatabaseName;
    private readonly string _containerName = settings.ContainerName;

    public async Task<Container> GetContainerAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName, 4000);
        return await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/partitionKey", 4000);
    }
}