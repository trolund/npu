using NPU.Data.Models;

namespace NPU.Data.Repositories;

public interface INpuRepository
{
    Task<Npu> UpdateNpuAsync(Npu npu);
    Task<Npu> CreateNpuAsync(Npu npu);
    Task<Npu?> GetNpuAsync(string id);
    Task<IEnumerable<Npu>> GetAllNpusAsync();
    Task DeleteNoteAsync(string id);

    /// <summary>
    /// query the repository with pagination
    /// </summary>
    /// <param name="searchTerm"></param>
    /// <param name="sortOrderKey"></param>
    /// <param name="ascending"></param>
    /// <param name="offset"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<(IEnumerable<Npu> Items, int)> QueryWithPagination(
        string? searchTerm, 
        int offset, 
        int pageSize, 
        bool ascending, 
        string? sortOrderKey);

    Task<(IEnumerable<Npu> Items, int)> GetNpusPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey);
}