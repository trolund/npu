using LockNote.Data.Model;
using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Base;

public class CosmosRepository<T>(ICosmosDbService cosmosDbService) : IRepository<T> 
    where T : BaseItem, new()
{
    private readonly Container _container = cosmosDbService.GetContainerAsync().GetAwaiter().GetResult();
    
    private static string GetKey()
    {
        var obj = new T();
        return obj.PartitionKey;
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(GetKey()));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(QueryDefinition query)
    {
        var queryIterator = _container.GetItemQueryIterator<T>(query);
        var results = new List<T>();

        while (queryIterator.HasMoreResults)
        {
            var response = await queryIterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<T> AddAsync(T entity)
    {
        return (await _container.CreateItemAsync(entity)).Resource;
    }

    public async Task<T> UpdateAsync(string id, T entity)
    {
       var obj = await _container.UpsertItemAsync(entity, new PartitionKey(id));
       return obj.Resource;
    }

    public async Task DeleteAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(GetKey()));
    }
}
