using System.Net;
using NPU.Data.Config;
using NPU.Data.DataHandlers;

namespace NPU.Bl;

public class FileUploadService(IBlobStorageDriver storageDriver, StorageSettings storageSettings)
{
    private static readonly string ImagePath = "images/npu";

    public async Task<string> UploadFileAsync(string id, string fileName, Stream fileStream)
    {
        var filePath = $"{id}/{DateTime.Now:yyyyMMddHHmmss}_{fileName}";
        await storageDriver.WriteFileAsync(ImagePath, filePath, fileStream);
        return filePath;
    }

    public async Task<Stream> GetFileAsync(string id, string path)
    {
        return await storageDriver.ReadFileAsync($"{ImagePath}/{id}/{path}");
    }
}