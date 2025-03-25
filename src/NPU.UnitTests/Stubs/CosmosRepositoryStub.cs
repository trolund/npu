using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Azure.Cosmos;
using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.UnitTests.Stubs;

public class CosmosRepositoryStub<T> : IRepository<T> where T : BaseItem
{
    private readonly List<T> _items = [];

    public async Task<T> AddAsync(T entity)
    {
        _items.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return await Task.FromResult(item);
    }

    public async Task<T> UpdateAsync(string id, T entity)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index >= 0)
        {
            _items[index] = entity;
            return await Task.FromResult(entity);
        }

        throw new KeyNotFoundException("Item not found");
    }

    public async Task<T> DeleteAsync(string id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _items.Remove(item);
            return await Task.FromResult(item);
        }

        throw new KeyNotFoundException("Item not found");
    }

    public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(_items.AsQueryable().Where(predicate));
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_items);
    }

    public async Task<(IEnumerable<T> Items, int)> QueryWithPaginationAsync(
        Expression<Func<T, bool>>? filterPredicate,
        string? sortOrderKey,
        bool ascending = true,
        int offset = 0,
        int pageSize = 10)
    {
        var query = _items.AsQueryable();
        if (filterPredicate != null)
        {
            query = query.Where(filterPredicate);
        }

        if (!string.IsNullOrEmpty(sortOrderKey))
        {
            query = ApplySorting(query, sortOrderKey, ascending);
        }

        var count = query.Count();
        var items = query.Skip(offset * pageSize).Take(pageSize).ToList();
        return await Task.FromResult((items, count));
    }

    private static IQueryable<T> ApplySorting(IQueryable<T> query, string sortOrderKey, bool ascending)
    {
        var property = typeof(T).GetProperty(sortOrderKey,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
            throw new ArgumentException($"Property {sortOrderKey} not found on type {typeof(T).Name}");
        }

        return ascending
            ? query.OrderBy(x => property.GetValue(x))
            : query.OrderByDescending(x => property.GetValue(x));
    }

    public Task<IEnumerable<TX>> QueryAsync<TX>(QueryDefinition queryDefinition)
    {
        throw new NotImplementedException("QueryDefinition is not supported in the stub.");
    }

    public Container GetContainer()
    {
        throw new NotImplementedException("CosmosDB Container is not available in the stub.");
    }
}