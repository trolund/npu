using System.ComponentModel.DataAnnotations;
using LockNote.Data.Model;

namespace LockNote.Infrastructure.Dtos;

public class NoteDto
{
    public string? Id { get; private init; }
    public int ReadBeforeDelete { get; init; }
    
    [MaxLength(10000, ErrorMessage = "Message should not be longer the {10000} chars")]
    public required string Content { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string? Password { get; set; }
    public static NoteDto FromModel(Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            ReadBeforeDelete = note.ReadBeforeDelete,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }
}