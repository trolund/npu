using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NPU.ApiTests.TestHelpers;

public abstract class TestBase(WebApiApplication factory) : IClassFixture<WebApiApplication>
{
    protected readonly HttpClient ApiClient = factory.CreateClient();

    protected async Task<(HttpResponseMessage, TX?)> PostAndSerialize<T, TX>(string url, T request)
    {
        var jsonContent = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await ApiClient.PostAsync(url, httpContent);
        var stringResult = await response.Content.ReadAsStringAsync();
        
        return (response, JsonSerializer.Deserialize<TX>(stringResult, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }));
    }

    protected async Task<(HttpResponseMessage, T?)> GetAndDeserialize<T>(string route)
    {
        var response = await ApiClient.GetAsync(route);
        response.EnsureSuccessStatusCode();
        var stringResult = await response.Content.ReadAsStringAsync();

        if (typeof(T) == typeof(string))
        {
            return (response, (T)(object)stringResult);
        }

        return (response, JsonSerializer.Deserialize<T>(stringResult, options: new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }));
    }
}