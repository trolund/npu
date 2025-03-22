using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.Data.Repositories;

public class NpuRepository(IRepository<Npu> npuRepository)
{
    public async Task<Npu> UpdateNpuAsync(Npu npu)
    {
        return await npuRepository.UpdateAsync(npu.Id, npu);
    }

    public async Task<Npu> CreateNpuAsync(Npu npu)
    {
        return await npuRepository.AddAsync(npu);
    }

    public async Task<Npu?> GetNpuAsync(string id)
    {
        return await npuRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Npu>> GetAllNpusAsync()
    {
        return await npuRepository.GetAllAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await npuRepository.DeleteAsync(id);
    }

    public async Task<(IEnumerable<Npu> Items, int)> GetNpusPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        return await npuRepository.QueryWithPaginationAsync(
            npu => string.IsNullOrEmpty(searchTerm) ||
                   npu.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || (npu.Description != null &&
                       npu.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)),
            sortOrderKey,
            ascending,
            offset: page - 1,
            pageSize);
    }
}