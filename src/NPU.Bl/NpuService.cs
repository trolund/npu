using Microsoft.Extensions.Logging;
using NPU.Data.Model;
using NPU.Data.Repositories;
using NPU.Infrastructure.Dtos;

namespace NPU.Bl;

public class NpuService(
    NpuRepository npuRepository,
    ScoreRepository scoreRepository,
    FileUploadService fileUploadService,
    ILogger<NpuService> logger)
{
    public async Task<Npu> CreateNpuWithImagesAsync(string name, string description,
        IEnumerable<(string, Stream)> images)
    {
        // optimistic file upload
        var links = new List<string>();
        foreach (var (fileName, stream) in images)
        {
            var link = await fileUploadService.UploadFileAsync(fileName, stream);
            links.Add(link);
        }

        return await npuRepository.CreateNpuAsync(new Npu()
        {
            Name = name,
            Description = description,
            Images = links.ToArray()
        });
    }

    public async Task<PaginatedResponse<NpuResponse>> GetNpuPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        var (items, totalCount) = await npuRepository
            .GetNpusPaginatedAsync(searchTerm, page, pageSize, ascending, sortOrderKey);

        return new PaginatedResponse<NpuResponse>(
            Items: items.Select(e => NpuResponse.FromModel(e)),
            TotalCount: totalCount,
            PageNumber: page,
            PageSize: pageSize,
            NumberOfPages: (int)Math.Ceiling((double)totalCount / pageSize)
        );
    }

    public async Task<Npu> UpdateNoteAsync(Npu npu)
    {
        return await npuRepository.UpdateNpuAsync(npu);
    }

    public async Task<IEnumerable<Npu>> GetAllNotesAsync()
    {
        return await npuRepository.GetAllNpusAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await npuRepository.DeleteNoteAsync(id);
    }

    public async Task<Score?> GiveScoreAsync(string npuId, CreateScoreRequest score)
    {
        var npu = await npuRepository.GetNpuAsync(npuId);
        if (npu == null)
        {
            return null;
        }

        return await scoreRepository.CreateScoreAsync(new Score
        {
            NpuId = npuId,
            Creativity = score.Creativity,
            Uniqueness = score.Uniqueness
        });
    }

    public async Task<NpuResponse?> GetNpuAsync(string id)
    {
        var score = await scoreRepository.GetAverageScoreForNpuIdAsync(id);
        var npu = await npuRepository.GetNpuAsync(id);
        return npu == null ? null : NpuResponse.FromModel(npu, score is null ? null : ScoreResponse.FromModel(score));
    }
}