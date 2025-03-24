using System.Net;
using NPU.Data.Config;
using NPU.Data.DataHandlers;
using static System.Text.RegularExpressions.Regex;

namespace NPU.Bl;

public partial class FileUploadService(IBlobStorageDriver storageDriver, StorageSettings storageSettings)
{
    private const string ImagePath = "images/npu";

    [System.Text.RegularExpressions.GeneratedRegex("^[a-zA-Z0-9_-]+$")]
    private static partial System.Text.RegularExpressions.Regex SanitizeRegex();

    private static string Sanitize(string id, string filename)
    {
        // Ensure 'id' is safe (only allow alphanumeric and underscores)
        var tryParse = Guid.TryParse(id, out _);

        if (!tryParse && !SanitizeRegex().IsMatch(id))
        {
            throw new ArgumentException("Invalid ID format.");
        }

        // Sanitize file name to prevent directory traversal
        var sanitizedFileName = Path.GetFileName(filename); // Removes any path components
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("Invalid file name.");
        }

        return sanitizedFileName;
    }

    public async Task<string> UploadFileAsync(string id, string fileName, Stream fileStream)
    {
        var sanitizedFileName = Sanitize(id, fileName);
        
        var filePath = $"{id}/{DateTime.Now:yyyyMMddHHmmss}_{sanitizedFileName}";
        await storageDriver.WriteFileAsync(ImagePath, filePath, fileStream);
        return filePath;
    }

    public async Task<Stream> GetFileAsync(string id, string filename)
    {
        var sanitizedFileName = Sanitize(id, filename);
        return await storageDriver.ReadFileAsync($"{ImagePath}/{id}/{sanitizedFileName}");
    }

}
