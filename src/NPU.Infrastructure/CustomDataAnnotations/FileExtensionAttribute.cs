using Microsoft.AspNetCore.Http;

namespace NPU.Infrastructure.CustomDataAnnotations;

using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class FileExtensionAttribute(string allowedFileTypes) : ValidationAttribute
{
    private string[] AllowedFileTypes { get; } = allowedFileTypes
        .ToLowerInvariant()
        .Split(',')
        .Select(ext => ext.Trim())
        .ToArray();
    private new string ErrorMessage => ErrorMessageString + "The file type is not allowed. Allowed types: {1}.";

    public override bool IsValid(object? value)
    {
        return value switch
        {
            null => false,
            IFormFile file => CheckFileType(file.FileName),
            _ => false
        };
    }
    
    private bool CheckFileType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return AllowedFileTypes.Contains(ext, StringComparer.OrdinalIgnoreCase);
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(CultureInfo.InvariantCulture,
            ErrorMessage, name, string.Join(", ", AllowedFileTypes));
    }

}