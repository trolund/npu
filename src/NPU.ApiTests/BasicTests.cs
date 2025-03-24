using Microsoft.AspNetCore.Mvc.Testing;
using NPU.ApiTests.TestHelpers;

namespace NPU.ApiTests;

public class BasicTests 
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(Routes.Npus.Test);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        
        Assert.Equal("Test Works!!", await response.Content.ReadAsStringAsync());
    }
}