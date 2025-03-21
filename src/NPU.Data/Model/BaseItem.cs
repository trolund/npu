using NanoidDotNet;
using Newtonsoft.Json;

namespace NPU.Data.Model;

public abstract class BaseItem
{
    [JsonProperty("id")] 
    public string Id { get; set; } = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, 10);

    [JsonProperty("partitionKey")] 
    public abstract string PartitionKey { get; protected init; }
}