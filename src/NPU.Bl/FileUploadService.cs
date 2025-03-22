using NPU.Data.DataHandlers;

namespace NPU.Bl;

public class FileUploadService(IBlobStorageDriver storageDriver)
{
    private static readonly string Path = $"{Directory.GetCurrentDirectory()}/images/npu";

    public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
    {
        var filePath = $"{Path}/{fileName}";
        await storageDriver.WriteFileAsync(filePath, fileStream);
        return filePath;
    }
}