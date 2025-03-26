using Newtonsoft.Json;

namespace NPU.Data.Models;

public abstract class BaseItem
{
    protected BaseItem()
    {
        PartitionKey = GetType().Name.ToLower();
        Id ??= Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Identifier for the item
    /// </summary>
    [JsonProperty("id")]
    // only set id if it's not set
    public string Id { get; set; }
    
    /// <summary>
    /// Partition key for the item
    /// Each item in the Cosmos DB must have a unique partition key
    /// </summary>
    [JsonProperty("partitionKey")] 
    public string PartitionKey { get; init; }   
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}