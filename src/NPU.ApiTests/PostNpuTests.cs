using System.Net;
using NPU.ApiTests.TestHelpers;
using NPU.Infrastructure.Dtos;

namespace NPU.ApiTests;

public class PostNpuTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task GIVEN_InvalidCreateNpuRequest_WHEN_PostNpu_THEN_400IsReturned()
    {
        // GIVEN
        var invalidRequest = new CreateNpuRequest
        {
            Name = "", // Invalid: Required field is empty
            Description = "This is a valid description",
            Images = [] 
        };
        
        // WHEN
        var (httpMsg, data) = await PostAndSerialize<CreateNpuRequest, dynamic>(Routes.Npus.Create, invalidRequest);

        // THEN
        Assert.Equal(HttpStatusCode.BadRequest, httpMsg.StatusCode);
    }
}