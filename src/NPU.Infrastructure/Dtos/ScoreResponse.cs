using NPU.Data.Model;

namespace NPU.Infrastructure.Dtos;

public class ScoreResponse
{
    public required int Creativity { get; set; }
    public required int Uniqueness { get; set; }
    
    public static ScoreResponse FromModel(Score score) => new()
    {
        Creativity = score.Creativity,
        Uniqueness = score.Uniqueness
    };
}