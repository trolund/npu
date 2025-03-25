using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NPU.ApiTests.TestHelpers;
using NPU.Infrastructure.Dtos;

namespace NPU.ApiTests;

public class GetNpuByIdTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task GIVEN_AUserInTheDB_WHEN_ANpuIsGetById_THEN_TheUserIsReturned_Correctly()
    {
        // Arrange
        var npu = DatabaseSeeding.Npus.First();
        
        // Act
        var (_, data) = await GetAndDeserialize<NpuResponse>(Routes.Npus.Get(npu.Id));

        // Assert
        Assert.NotNull(data);
        Assert.Equal(npu.Id, data.Id);
        Assert.Equal(npu.Name, data.Name);
        Assert.Equal(npu.Description, data.Description);
        Assert.Equal(npu.Images, data.Images);
    }
    
    [Fact]
    public async Task GIVEN_AUserIsNotInTheDB_WHEN_ANpuIsGetById_THEN_ThenA404IsReturned()
    {
        // Arrange
        const string npuId = "00000000-0000-0000-0000-000000000000";
        
        // Act
        var result = await ApiClient.GetAsync(Routes.Npus.Get(npuId));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
    
    [Fact]
    public async Task GIVEN_InvalidCreateNpuRequest_WHEN_PostNpu_THEN_400IsReturned()
    {
        // Arrange
        var invalidRequest = new CreateNpuRequest
        {
            Name = "", // Invalid: Required field is empty
            Description = "This is a valid description",
            Images = [] 
        };
        
        // Act
        var (httpMsg, data) = await PostAndSerialize<CreateNpuRequest, dynamic>(Routes.Npus.Create, invalidRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, httpMsg.StatusCode);
    }
}