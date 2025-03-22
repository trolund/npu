using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NPU.Infrastructure.CustomDataAnnotations;

/// <summary>
/// Validation attribute to enforce a maximum file size for uploaded files.
/// The file size is specified in megabytes (MB).
/// </summary>
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxSizeInBytes;

    public MaxFileSizeAttribute(long maxSizeInMb)
    {
        _maxSizeInBytes = maxSizeInMb * 1024 * 1024; // Convert MB to Bytes
        ErrorMessage = $"The file size cannot exceed {maxSizeInMb} MB.";
    }

    public override bool IsValid(object? value)
    {
        if (value is not IFormFile file)
            return true; 

        return file.Length <= _maxSizeInBytes;
    }
}