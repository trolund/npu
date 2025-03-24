using NPU.ApiTests.TestHelpers;

namespace NPU.ApiTests;

public class SmokeTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task SmokeTest()
    {
        // Act
        var result = await GetAndDeserialize<string>(Routes.Health);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Healthy", result);
    }
}