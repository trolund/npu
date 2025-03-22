using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.Data.Repositories;

public class NpuRepository(IRepository<Npu> notesRepository)
{
    public async Task<Npu> UpdateAsync(Npu npu)
    {
        return await notesRepository.UpdateAsync(npu.Id, npu);
    }

    public async Task<Npu> CreateNoteAsync(Npu npu)
    {
        return await notesRepository.AddAsync(npu);
    }

    public async Task<Npu?> GetNoteAsync(string id)
    {
        return await notesRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Npu>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await notesRepository.DeleteAsync(id);
    }

    public async Task<(IEnumerable<Npu> Items, int)> GetNpuPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        return await notesRepository.QueryWithPaginationAsync(
            npu => string.IsNullOrEmpty(searchTerm) ||
                   npu.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || (npu.Description != null &&
                       npu.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)),
            sortOrderKey,
            ascending,
            offset: page - 1,
            pageSize);
    }
}