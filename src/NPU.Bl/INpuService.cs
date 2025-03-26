using NPU.Data.Model;
using NPU.Infrastructure.Dtos;

namespace NPU.Bl;

public interface INpuService
{
    Task<NpuResponse?> CreateNpuWithImagesAsync(string name, string description,
        IEnumerable<(string, Stream)> images);

    Task<(Stream?, string?)> GetImageOfNpu(string id, string filename);

    Task<PaginatedResponse<NpuResponse>> GetNpuPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey);

    Task<Npu> UpdateNoteAsync(Npu npu);
    Task<IEnumerable<Npu>> GetAllNotesAsync();
    Task DeleteNoteAsync(string id);
    Task<ScoreResponse?> CreateScoreOfNpuAsync(string npuId, CreateScoreRequest score);
    Task<NpuResponse?> GetNpuAsync(string id);
}