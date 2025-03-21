using System.ComponentModel.DataAnnotations;

namespace LockNote.Data.Model;

public class Note : BaseItem
{
    [Range(1, int.MaxValue, ErrorMessage = "Read before delete must be bigger than {1}")]
    public int ReadBeforeDelete { get; set; } = 1;
    
    [MaxLength(10000, ErrorMessage = "Message should not be longer the {10000} chars")]
    public required string Content { get; set; }
    
    public required DateTime CreatedAt { get; init; }
    
    public string? PasswordHash { get; set; }
    public byte[]? Salt { get; set; }
    public override string PartitionKey { get; protected init; } = "Note";
}
