using NPU.ApiTests.TestHelpers;
using NPU.Data.Model;
using NPU.Infrastructure.Dtos;

namespace NPU.ApiTests.Tests;

public class GetNpusTests(WebApiApplication factory) : TestBase(factory)
{
    [Theory]
    [InlineData("a", nameof(Npu.CreatedAt), true, 1, 1)]
    [InlineData("b", nameof(Npu.CreatedAt), true, 1, 2)]
    [InlineData("a", nameof(Npu.CreatedAt), true, 1, 3)]
    public async Task GIVEN_NpusInTheDB_WHEN_GetNpusIsCalled_THEN_NpusAreReturned_Correctly(
        string searchTerm, string sortOrderKey, bool ascending, int page, int pageSize)
    {
        // WHEN
        var (_, data) = await GetAndDeserialize<PaginatedResponse<NpuResponse>>(
            Routes.Npus.Get(page, pageSize, sortOrderKey, ascending, searchTerm));

        // THEN
        Assert.NotNull(data);
        Assert.NotNull(data.Items);
        Assert.NotEmpty(data.Items);
        Assert.Equal(pageSize, data.Items.Count());
        Assert.Equal(pageSize, data.PageSize);
        Assert.True(data.Items.All(npu => npu.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
    }
}