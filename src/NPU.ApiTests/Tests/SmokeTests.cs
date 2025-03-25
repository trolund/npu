using NPU.ApiTests.TestHelpers;

namespace NPU.ApiTests.Tests;

public class SmokeTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task WHEN_ServiceIsRunning_THEN_ItIsHealthy()
    {
        // Act
        var (_, result) = await GetAndDeserialize<string>(Routes.Health);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Healthy", result);
    }
}