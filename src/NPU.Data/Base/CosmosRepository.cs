using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using NPU.Data.Model;

namespace NPU.Data.Base;

public class CosmosRepository<T>(ICosmosDbService cosmosDbService) : IRepository<T> where T : BaseItem
{
    private readonly Container _container = cosmosDbService.GetContainerAsync().GetAwaiter().GetResult();
    private readonly string _key = typeof(T).Name.ToLower();
    
    public async Task<T> AddAsync(T entity)
    {
        return (await _container.CreateItemAsync(entity, new PartitionKey(_key))).Resource;
    }
    
    public async Task<T?> GetByIdAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(_key));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    
    public async Task<T> UpdateAsync(string id, T entity)
    {
        return (await _container.ReplaceItemAsync(entity, id, new PartitionKey(_key))).Resource;
    }
    
    public async Task<T> DeleteAsync(string id)
    {
        return (await _container.DeleteItemAsync<T>(id, new PartitionKey(_key))).Resource;
    }
    
    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
    {
        var query = _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: false)
            .Where(x => x.PartitionKey == _key)
            .Where(predicate)
            .ToFeedIterator();

        List<T> results = [];
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }
        return results;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: false)
            .Where(x => x.PartitionKey == _key)
            .ToFeedIterator();
        
        List<T> results = [];
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }
        return results;
    }
    
    /// <summary>
    /// query the repository with pagination
    /// WARNING: This method is not efficient and should not be used in production
    /// </summary>
    /// <param name="filterPredicate"></param>
    /// <param name="sortOrderKey"></param>
    /// <param name="ascending"></param>
    /// <param name="offset"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<T> Items, int)> QueryWithPaginationAsync(
        Expression<Func<T, bool>>? filterPredicate,
        string? sortOrderKey,
        bool ascending = true,
        int offset = 0,
        int pageSize = 10)
    {
        // Do everything in memory :(
        var query = (await GetAllAsync()).AsQueryable();
        
        if (!string.IsNullOrEmpty(sortOrderKey))
        {
            query = ApplySorting(query, sortOrderKey, ascending);
        }

        if (filterPredicate != null)
        {
            query = query.Where(filterPredicate);
        }

        var count = await query.CountAsync();
        var items = query
            .Skip(offset * pageSize)
            .Take(pageSize)
            .ToList();

        return (items, count.Resource);
    }
    
    private static IQueryable<T> ApplySorting(IQueryable<T> query, string sortOrderKey, bool ascending)
    {
        var property = typeof(T).GetProperty(sortOrderKey, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new ArgumentException($"Property {sortOrderKey} not found on type {typeof(T).Name}");
        }

        return ascending
            ? query.OrderBy(x => property.GetValue(x))
            : query.OrderByDescending(x => property.GetValue(x));
    }
    
    public async Task<IEnumerable<TX>> QueryAsync<TX>(QueryDefinition queryDefinition)
    {
        var query = _container.GetItemQueryIterator<TX>(queryDefinition);
        List<TX> results = [];
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }
        return results;
    }
    
    public Container GetContainer()
    {
        return _container;
    }

}