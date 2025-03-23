using System.ComponentModel.DataAnnotations;

namespace NPU.Infrastructure.Dtos;

public record CreateScoreRequest
{
    [Required]
    public required string NpuId { get; set; }

    [Required] 
    [Range(1, 5)]
    public required int Creativity { get; init; } = 1;
    
    [Required] 
    [Range(1, 5)]
    public required int Uniqueness { get; init; } = 1;
}