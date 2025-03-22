using NanoidDotNet;
using Newtonsoft.Json;

namespace NPU.Data.Model;

public abstract class BaseItem
{
    protected BaseItem()
    {
        PartitionKey = GetType().Name.ToLower();
    }

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
    private string PartitionKey { get; init; }   
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}