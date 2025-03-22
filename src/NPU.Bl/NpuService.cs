using Microsoft.Extensions.Logging;
using NPU.Data.Model;
using NPU.Data.Repositories;
using NPU.Infrastructure.Dtos;

namespace NPU.Bl;

public class NpuService(NpuRepository notesRepository, FileUploadService fileUploadService, ILogger<NpuService> logger)
{
    public async Task<Npu> CreateNpuAsync(Npu npu)
    {
        return await notesRepository.CreateNoteAsync(npu);
    }
    
    public async Task<Npu> CreateNpuWithImagesAsync(string name, string description, IEnumerable<(string, Stream)> images)
    {
        var links = new List<string>();
        foreach (var (fileName, stream) in images)
        {
            var link = await fileUploadService.UploadFileAsync(fileName, stream);
            links.Add(link);
        }
        
        return await notesRepository.CreateNoteAsync(new Npu()
        {
            Name = name,
            Description = description,
            File = links.ToArray()
        });
    }

    public async Task<PaginatedResponse<Npu>> GetNpuPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        var (items, totalCount) = await notesRepository
            .GetNpuPaginatedAsync(searchTerm, page, pageSize, ascending, sortOrderKey);

        return new PaginatedResponse<Npu>(
            Items: items,
            TotalCount: totalCount,
            PageNumber: page,
            PageSize: pageSize,
            NumberOfPages: (int)Math.Ceiling((double)totalCount / pageSize)
        );
    }

    public async Task<Npu> UpdateNoteAsync(Npu npu)
    {
        return await notesRepository.UpdateAsync(npu);
    }

    public async Task<IEnumerable<Npu>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllNotesAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await notesRepository.DeleteNoteAsync(id);
    }
}