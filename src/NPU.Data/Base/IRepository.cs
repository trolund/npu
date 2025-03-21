using Microsoft.Azure.Cosmos;
using NPU.Data.Model;

namespace NPU.Data.Base;

public interface IRepository<T> where T : BaseItem
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync(QueryDefinition query);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}