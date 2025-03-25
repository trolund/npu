using System.Net;
using NPU.ApiTests.TestHelpers;
using NPU.Infrastructure.Dtos;

namespace NPU.ApiTests;

public class GetNpuByIdTests(WebApiApplication factory) : TestBase(factory)
{
    [Fact]
    public async Task GIVEN_AUserInTheDB_WHEN_ANpuIsGetById_THEN_TheUserIsReturned_Correctly()
    {
        // GIVEN
        var npu = DatabaseSeeding.Npus.First();
        
        // WHEN
        var (_, data) = await GetAndDeserialize<NpuResponse>(Routes.Npus.Get(npu.Id));

        // THEN
        Assert.NotNull(data);
        Assert.Equal(npu.Id, data.Id);
        Assert.Equal(npu.Name, data.Name);
        Assert.Equal(npu.Description, data.Description);
        Assert.Equal(npu.Images, data.Images);
    }
    
    [Fact]
    public async Task GIVEN_AUserIsNotInTheDB_WHEN_ANpuIsGetById_THEN_ThenA404IsReturned()
    {
        // GIVEN
        const string npuId = "00000000-0000-0000-0000-000000000000";
        
        // WHEN
        var result = await ApiClient.GetAsync(Routes.Npus.Get(npuId));

        // THEN
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
    
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