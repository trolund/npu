using NPU.Data.Model;

namespace NPU.Infrastructure.Dtos;

public class ScoreSummeryResponse
{
    public double AvgCreativity { get; set; }
    public double AvgUniqueness { get; set; }
    
    public static ScoreSummeryResponse FromModel(ScoreSummery score) => new()
    {
        AvgUniqueness = score.AvgUniqueness,
        AvgCreativity = score.AvgCreativity
    };
}