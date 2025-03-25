using NPU.Data.Model;

namespace NPU.Data.Repositories;

public interface IScoreRepository
{
    Task<Score?> UpdateScoreAsync(Score score);
    Task<Score?> CreateScoreAsync(Score score);
    Task<Score?> GetScoreAsync(string id);
    Task<IEnumerable<Score?>> GetAllScoresAsync();
    Task DeleteNoteAsync(string id);
    Task<ScoreSummery?> GetAverageScoreForNpuIdAsync(string npuId);
}