using System.ComponentModel.DataAnnotations;

namespace NPU.Data.Model;

public class Npu : BaseItem
{
    [Required]
    [StringLength(70, ErrorMessage = "Message should not be longer the {70} chars")]
    public required string Name { get; init; }
    
    [StringLength(200, ErrorMessage = "Message should not be longer the {200} chars")]
    public string? Description { get; init; }
    
    public required string[] Images { get; init; } = [];
    
    // public override string PartitionKey { get; protected init; } = "npu";
}
