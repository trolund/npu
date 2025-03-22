using System.Linq.Expressions;
using NPU.Data.Model;

namespace NPU.Data.Base;

public interface IRepository<T> where T : BaseItem
{
    /// <summary>
    /// Get all items from the repository of type T
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Query the repository of type T with a LINQ expression
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Delete an item from the repository of type T
    /// </summary>
    Task<T> DeleteAsync(string id);

    /// <summary>
    /// Update an item in the repository of type T
    /// </summary>
    Task<T> UpdateAsync(string id, T entity);

    /// <summary>
    /// Get an item from the repository of type T by its ID
    /// </summary>
    Task<T?> GetByIdAsync(string id);

    /// <summary>
    /// Add an item to the repository of type T
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Query the repository of type T with pagination
    /// </summary>
    /// <param name="filterPredicate">The predicate to filter the items</param>
    /// <param name="sortOrderKey">The key to sort the items by</param>
    /// <param name="ascending">Whether to sort the items in ascending order</param>
    /// <param name="offset">The offset to start the pagination from</param>
    /// <param name="pageSize">The number of items to return</param>
    /// <returns>A tuple containing the items and the total number of items</returns>
    Task<(IEnumerable<T> Items, int)> QueryWithPaginationAsync(
        Expression<Func<T, bool>> filterPredicate,
        string? sortOrderKey,
        bool ascending = true,
        int offset = 0,
        int pageSize = 10);
}