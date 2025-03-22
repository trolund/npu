using NanoidDotNet;
using Newtonsoft.Json;

namespace NPU.Data.Model;

public abstract class BaseItem
{
    /// <summary>
    /// Identifier for the item
    /// </summary>
    [JsonProperty("id")] 
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Partition key for the item
    /// Each item in the Cosmos DB must have a unique partition key
    /// </summary>
    [JsonProperty("partitionKey")] 
    public abstract string PartitionKey { get; protected init; }
}