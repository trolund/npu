using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using NPU.Data.Config;

namespace NPU.Data.DataHandlers;

public class BlobStorageDriver(StorageSettings config): IBlobStorageDriver
{
    private readonly string _connectionString = $"DefaultEndpointsProtocol=https;AccountName={config.ACCOUNT_NAME};AccountKey={config.CONNECTION_KEY};EndpointSuffix=core.windows.net";
    
    public async Task<Stream> ReadFileAsync(string blobName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(config.CON_NAME);
        var blobClient = containerClient.GetBlobClient(blobName);

        BlobDownloadInfo download = await blobClient.DownloadAsync();
        
        return download.Content;
    }
    
    public async Task WriteFileAsync(string path, string fileName, Stream data)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(config.CON_NAME);
        var blobClient = containerClient.GetBlobClient(Path.Combine(path, fileName));
        
        await blobClient.UploadAsync(data);
    }
}