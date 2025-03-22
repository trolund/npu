using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using NPU.Data.Model;

namespace NPU.Data.Base;

public class CosmosRepository<T>(ICosmosDbService cosmosDbService) : IRepository<T> where T : BaseItem
{
    private readonly Container _container = cosmosDbService.GetContainerAsync().GetAwaiter().GetResult();

    // CREATE
    public async Task<T> AddAsync(T entity)
    {
        return (await _container.CreateItemAsync(entity, new PartitionKey((entity as dynamic).Id))).Resource;
    }

    // READ by ID
    public async Task<T?> GetByIdAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // UPDATE
    public async Task<T> UpdateAsync(string id, T entity)
    {
        return (await _container.ReplaceItemAsync(entity, id, new PartitionKey(id))).Resource;
    }

    // DELETE
    public async Task<T> DeleteAsync(string id)
    {
        return (await _container.DeleteItemAsync<T>(id, new PartitionKey(id))).Resource;
    }

    // QUERY with LINQ
    public async Task<IEnumerable<T>> QueryAsync<T>(Expression<Func<T, bool>> predicate)
    {
        var query = _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: false)
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
            .ToFeedIterator();

        List<T> results = [];
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }
        return results;
    }
    
    public async Task<(IEnumerable<T> Items, int)> QueryWithPaginationAsync(
        Expression<Func<T, bool>> filterPredicate,
        string? sortOrderKey,
        bool ascending = true,
        int offset = 0,
        int pageSize = 10)
    {
        var query = _container
            .GetItemLinqQueryable<T>(allowSynchronousQueryExecution: false)
            .AsQueryable();
        
        // Apply sorting if provided
        if (!string.IsNullOrEmpty(sortOrderKey))
        {
            query = ApplySorting(query, sortOrderKey, ascending);
        }
        
        query = query.Where(filterPredicate);
        
        var count = (await query.CountAsync()).Resource;
        var items = query
            .Skip(offset * pageSize)
            .Take(pageSize)
            .ToList();

        return (items, count);
    }
    
    private static IQueryable<T> ApplySorting(IQueryable<T> query, string sortOrderKey, bool ascending)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = typeof(T).GetProperty(sortOrderKey, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
        {
            throw new ArgumentException($"Property '{sortOrderKey}' not found in type '{typeof(T).Name}'");
        }

        var keySelector = Expression.Lambda(Expression.Property(param, property), param);
        var methodName = ascending ? "OrderBy" : "OrderByDescending";

        var sortedQuery = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.PropertyType)
            .Invoke(null, new object[] { query, keySelector });

        return (IQueryable<T>)sortedQuery!;
    }

}