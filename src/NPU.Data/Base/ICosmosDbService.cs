using Microsoft.Azure.Cosmos;

namespace NPU.Data.Base;

public interface ICosmosDbService
{
    Task<Container> GetContainerAsync();
    Task EnsureDbSetupAsync();
    Task RemoveDbSetupAsync();
}