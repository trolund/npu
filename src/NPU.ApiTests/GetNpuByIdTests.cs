using NPU.ApiTests.TestHelpers;
using NPU.Infrastructure.Dtos;

namespace NPU.ApiTests;

public class GetNpuByIdTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task GetNpuById()
    {
        // Arrange
        var npu = DatabaseSeeding.Npus.First();
        
        // Act
        var result = await GetAndDeserialize<NpuResponse>(Routes.Npus.Get(npu.Id));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(npu.Id, result.Id);
        Assert.Equal(npu.Name, result.Name);
        Assert.Equal(npu.Description, result.Description);
        Assert.Equal(npu.Images, result.Images);
    }
}