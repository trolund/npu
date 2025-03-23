namespace NPU.Infrastructure.Dtos;

public class ScoreResponse
{
    public double AvgCreativity { get; set; }
    public double AvgUniqueness { get; set; }
    
    public static ScoreResponse FromModel(ScoreSummery score) => new()
    {
        AvgUniqueness = score.AvgUniqueness,
        AvgCreativity = score.AvgCreativity
    };
}