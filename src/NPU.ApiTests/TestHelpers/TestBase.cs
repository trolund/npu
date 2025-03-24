using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using program = Microsoft.VisualStudio.TestPlatform.TestHost.Program;

namespace NPU.ApiTests.TestHelpers;

public abstract class TestBase(WebApplicationFactory<program> factory) : IClassFixture<WebApplicationFactory<program>>
{
    protected readonly HttpClient ApiClient = factory.CreateClient();

    protected async Task<T?> GetAndDeserialize<T>(string route)
    {
        var response = await ApiClient.GetAsync(route);
        response.EnsureSuccessStatusCode();
        var stringResult = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(stringResult);
        return result;
    }
}