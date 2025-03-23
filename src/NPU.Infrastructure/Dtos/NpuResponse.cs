using NPU.Data.Model;

namespace NPU.Infrastructure.Dtos;

public class NpuResponse
{
    public required string Id { get; set; }
    public required string Name { get; init; }
    
    public string? Description { get; init; }
    
    public required string[] Images { get; init; } = [];
    
    public ScoreResponse? Score { get; init; }
    
    public static NpuResponse FromModel(Npu npu, ScoreResponse? score = null) => new()
    {
        Id = npu.Id,
        Name = npu.Name,
        Description = npu.Description,
        Images = npu.Images,
        Score = score
    };
}