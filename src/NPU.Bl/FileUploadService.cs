using NPU.Data.DataHandlers;

namespace NPU.Bl;

public class FileUploadService(IBlobStorageDriver storageDriver)
{
    private static readonly string Path = $"{Directory.GetCurrentDirectory()}/images/npu";

    public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
    {
        var filePath = $"{Path}/{DateTime.Now:yyyyMMddHHmmss}_{fileName}";
        await storageDriver.WriteFileAsync(Path, fileName, fileStream);
        return filePath;
    }
}