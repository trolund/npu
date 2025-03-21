using LockNote.Data.Model;
using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Base;

public interface IRepository<T> where T : BaseItem
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync(QueryDefinition query);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}