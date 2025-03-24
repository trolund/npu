using Microsoft.AspNetCore.Mvc.Testing;
using NPU.ApiTests.TestHelpers;
using NPU.Data.Model;

namespace NPU.ApiTests;

public class GetNpuByIdTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task GetNpuById()
    {
        // Act
        var result = await GetAndDeserialize<string>(Routes.Npus.Test);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Works!!", result);
    }
}