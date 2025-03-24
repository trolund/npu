using NPU.Data.Config;
using NPU.Data.DataHandlers;

namespace NPU.Bl;

public class FileUploadService(IBlobStorageDriver storageDriver, StorageSettings storageSettings)
{
    private static readonly string ImagePath = "images/npu";

    public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
    {
        var filePath = $"{ImagePath}/{DateTime.Now:yyyyMMddHHmmss}_{fileName}";
        await storageDriver.WriteFileAsync(ImagePath, filePath, fileStream);
        return Path.Combine(storageSettings.CON_URL, filePath);
    }
}