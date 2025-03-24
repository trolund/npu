using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NPU.ApiTests.TestHelpers;

public abstract class TestBase : IClassFixture<WebApiApplication>
{
    protected readonly HttpClient ApiClient;

    protected TestBase(WebApiApplication factory)
    {
        ApiClient = factory.CreateClient();
    }

    protected async Task<T?> GetAndDeserialize<T>(string route)
    {
        var response = await ApiClient.GetAsync(route);
        response.EnsureSuccessStatusCode();
        var stringResult = await response.Content.ReadAsStringAsync();

        if (typeof(T) == typeof(string))
        {
            return (T)(object)stringResult;
        }

        return JsonSerializer.Deserialize<T>(stringResult, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}