using NPU.Data.Models;
using NPU.Data.Repositories;
using NPU.Infrastructure.Dtos;

namespace NPU.Bl;

public class NpuService(
    INpuRepository npuRepository,
    IScoreRepository scoreRepository,
    IFileUploadService fileUploadService) : INpuService
{
    public async Task<NpuResponse?> CreateNpuWithImagesAsync(string name, string description,
        IEnumerable<(string, Stream)> images)
    {
        var id = Guid.NewGuid().ToString();
        
        // TODO: Optimistic file upload
        var links = new List<string>();
        foreach (var (fileName, stream) in images)
        {
            var link = await fileUploadService.UploadFileAsync(id, fileName, stream);
            links.Add(link);
        }

        var created = await npuRepository.CreateNpuAsync(new Npu()
        {
            Id = id,
            Name = name,
            Description = description,
            Images = links.ToArray()
        });

        return NpuResponse.FromModel(created);
    }

    // TODO: Insecure read
    public async Task<(Stream?, string?)> GetImageOfNpu(string id, string filename)
    {
        try
        {
            var stream = await fileUploadService.GetFileAsync(id, filename);
            var fileType = Path.GetExtension(filename).Replace(".", "");
            return (stream, fileType);
        }
        catch (Exception e)
        {
            return (null, null);
        }
    }

    public async Task<PaginatedResponse<NpuResponse>> GetNpuPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        var (items, totalCount) = await npuRepository
            .GetNpusPaginatedAsync(searchTerm, page, pageSize, ascending, sortOrderKey);
        
        // Enrich with score
        // TODO: This is a naive implementation, offload to a function app
        var mappedItems = await Task.WhenAll(items.Select(e => NpuResponse.FromModel(e))
            .Select(async response =>
            {
                var score = await GetScoreSummeryAsync(response.Id);
                if (score == null)
                {
                    return response;
                }

                response.Score = ScoreSummeryResponse.FromModel(score);
                return response;
            }));
        
        return new PaginatedResponse<NpuResponse>(
            Items: mappedItems,
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

    public async Task<ScoreResponse?> CreateScoreOfNpuAsync(string npuId, CreateScoreRequest score)
    {
        var npu = await npuRepository.GetNpuAsync(npuId);
        if (npu == null)
        {
            return null;
        }

        var created = await scoreRepository.CreateScoreAsync(new Score
        {
            NpuId = npuId,
            Creativity = score.Creativity,
            Uniqueness = score.Uniqueness
        });
        
        return created is null ? null : ScoreResponse.FromModel(created);
    }
    
    private async Task<ScoreSummery?> GetScoreSummeryAsync(string id)
    {
        return await scoreRepository.GetAverageScoreForNpuIdAsync(id);
    }

    public async Task<NpuResponse?> GetNpuAsync(string id)
    {
        var score = await scoreRepository.GetAverageScoreForNpuIdAsync(id);
        var npu = await npuRepository.GetNpuAsync(id);
        return npu is null ? null : NpuResponse.FromModel(npu, score is null ? null : ScoreSummeryResponse.FromModel(score));
    }
}