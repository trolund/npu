using NPU.Data.Models;

namespace NPU.Infrastructure.Dtos;

public class NpuResponse
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    
    public string? Description { get; init; }
    
    public required string[] Images { get; init; } = [];
    
    public ScoreSummeryResponse? Score { get; set; }
    
    public static NpuResponse FromModel(Npu npu, ScoreSummeryResponse? score = null) => new()
    {
        Id = npu.Id,
        Name = npu.Name,
        Description = npu.Description,
        Images = npu.Images,
        Score = score
    };
}