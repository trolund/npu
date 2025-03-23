using System.Linq.Expressions;
using Microsoft.Azure.Cosmos;
using NPU.Data.Base;
using NPU.Data.Model;

namespace NPU.Data.Repositories;

public class NpuRepository(IRepository<Npu> npuRepository)
{
    public async Task<Npu> UpdateNpuAsync(Npu npu)
    {
        return await npuRepository.UpdateAsync(npu.Id, npu);
    }

    public async Task<Npu> CreateNpuAsync(Npu npu)
    {
        return await npuRepository.AddAsync(npu);
    }

    public async Task<Npu?> GetNpuAsync(string id)
    {
        return await npuRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Npu>> GetAllNpusAsync()
    {
        return await npuRepository.GetAllAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        await npuRepository.DeleteAsync(id);
    }

    /// <summary>
    /// query the repository with pagination
    /// </summary>
    /// <param name="searchTerm"></param>
    /// <param name="sortOrderKey"></param>
    /// <param name="ascending"></param>
    /// <param name="offset"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Npu> Items, int)> QueryWithPagination(
        string? searchTerm, 
        int offset, 
        int pageSize, 
        bool ascending, 
        string? sortOrderKey)
    {
        // Default to sorting by "id" if no key is provided
        sortOrderKey ??= "id"; 

        var orderBy = ascending ? "ASC" : "DESC";
        var queryString = $@"
            SELECT * FROM c 
            WHERE c.partitionKey = 'npu' AND (@searchTerm = '' OR CONTAINS(c.Name, @searchTerm, true) OR CONTAINS(c.Description, @searchTerm, true)) 
            ORDER BY c.{sortOrderKey} {orderBy} 
            OFFSET @offset LIMIT @pageSize";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@searchTerm", searchTerm ?? "")
            .WithParameter("@offset", offset)
            .WithParameter("@pageSize", pageSize);

        var requestOptions = new QueryRequestOptions
        {
            MaxItemCount = pageSize
        };

        List<dynamic> items = [];
        using var resultSet = npuRepository.GetContainer().GetItemQueryIterator<Npu>(
            queryDefinition, requestOptions: requestOptions);

        while (resultSet.HasMoreResults)
        {
            var response = await resultSet.ReadNextAsync();
            items.AddRange(response);
        }
        
        List<Npu> items2 = [];

        return (items2, await GetItemCountAsync(searchTerm));
    }
    
    private async Task<int> GetItemCountAsync(string? searchTerm)
    {
        var query = new QueryDefinition(@"
        SELECT VALUE COUNT(1) FROM c 
        WHERE (@searchTerm = '' OR CONTAINS(c.Name, @searchTerm, true) OR CONTAINS(c.Description, @searchTerm, true))")
            .WithParameter("@searchTerm", searchTerm);

        using var iterator = npuRepository.GetContainer().GetItemQueryIterator<int>(query);

        if (!iterator.HasMoreResults) return 0; // No results found
        var response = await iterator.ReadNextAsync();
        return response.FirstOrDefault(); // COUNT returns a single integer value

    }

    public async Task<(IEnumerable<Npu> Items, int)> GetNpusPaginatedAsync(string? searchTerm, int page, int pageSize,
        bool ascending, string? sortOrderKey)
    {
        Expression<Func<Npu, bool>> filter = npu => 
            string.IsNullOrEmpty(searchTerm) ||
            npu.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
            (npu.Description != null && npu.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        
        return await npuRepository.QueryWithPaginationAsync(
            filter,
            sortOrderKey,
            ascending,
            offset: page - 1,
            pageSize);
    }
}