using Microsoft.Azure.Cosmos;
using NPU.Data.Base;
using NPU.Data.Models;

namespace NPU.Data.Repositories;

public class ScoreRepository(IRepository<Score> scoreRepository) : IScoreRepository
{
    public async Task<Score?> UpdateScoreAsync(Score score)
    {
        return await scoreRepository.UpdateAsync(score.Id, score);
    }

    public async Task<Score?> CreateScoreAsync(Score score)
    {
        return await scoreRepository.AddAsync(score);
    }

    public async Task<Score?> GetScoreAsync(string id)
    {
        return await scoreRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Score?>> GetAllScoresAsync()
    {
        return await scoreRepository.GetAllAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await scoreRepository.DeleteAsync(id);
    }
    
    public async Task<ScoreSummery?> GetAverageScoreForNpuIdAsync(string npuId)
    {
        var query = new QueryDefinition(@"
            SELECT c.NpuId, 
                   AVG(c.Creativity) AS AvgCreativity, 
                   AVG(c.Uniqueness) AS AvgUniqueness,
                   COUNT(c) AS NumberOfScores
            FROM c
            WHERE c.NpuId = @npuId
            GROUP BY c.NpuId
        ").WithParameter("@npuId", npuId);

        return (await scoreRepository.QueryAsync<ScoreSummery>(query)).FirstOrDefault();
    }
    
}