using Microsoft.AspNetCore.Mvc.Testing;
using NPU.ApiTests.TestHelpers;
using NPU.Data.Model;

namespace NPU.ApiTests;

public class GetNpuByIdTests(WebApplicationFactory<Microsoft.VisualStudio.TestPlatform.TestHost.Program> factory) : TestBase(factory)
{
    [Fact]
    public async Task GetNpuById()
    {
        // Act
        var result = await GetAndDeserialize<Npu>(Routes.Npus.Get(""));

        // Assert
        Assert.NotNull(result);

        var expectedAuthorName = "";
        Assert.Equal(expectedAuthorName, result.Name);
    }
}