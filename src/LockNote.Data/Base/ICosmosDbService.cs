using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Base;

public interface ICosmosDbService
{
    Task<Container> GetContainerAsync();
}