using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NPU.Infrastructure.CustomDataAnnotations;

namespace NPU.Infrastructure.Dtos;

public record CreateNpuRequest
{
    [Required]
    [StringLength(70, ErrorMessage = "Message should not be longer the {70} chars")]
    public required string Name { get; init; }
    
    [StringLength(200, ErrorMessage = "Message should not be longer the {200} chars")]
    public string? Description { get; init; }
    
    [Required]
    [MaxFileSize(2)]
    [FileExtension(".jpg , .png, .gif, .jpeg, .bmp, .svg, .heic")]
    [MaxLength(3)]
    public required IFormFile[] Images { get; init; }
}