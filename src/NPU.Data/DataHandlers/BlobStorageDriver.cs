using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using NPU.Infrastructure.Config;

namespace NPU.Data.DataHandlers;

public class BlobStorageDriver(StorageSettings config): IBlobStorageDriver
{
    private readonly string _connectionString = $"DefaultEndpointsProtocol=https;AccountName={config.ACCOUNT_NAME};AccountKey={config.CONNECTION_KEY};EndpointSuffix=core.windows.net";
    
    public async Task<string> ReadFileAsync(string blobName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(config.CON_NAME);
        var blobClient = containerClient.GetBlobClient(blobName);

        BlobDownloadInfo download = await blobClient.DownloadAsync();
        
        using var reader = new StreamReader(download.Content);
        return await reader.ReadToEndAsync();
    }
    
    public async Task WriteFileAsync(string blobName, Stream data)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(config.CON_NAME);
        var blobClient = containerClient.GetBlobClient(blobName);
        
        await blobClient.UploadAsync(data);
    }
}