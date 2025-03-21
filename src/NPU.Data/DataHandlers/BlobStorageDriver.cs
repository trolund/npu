using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace NPU.Data.DataHandlers;

public class BlobStorageDriver(string connectionKey, string accountName, string containerName): IBlobStorageDriver
{
    private readonly string _connectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={connectionKey};EndpointSuffix=core.windows.net";
    
    public async Task<string> ReadBlob(string blobName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        BlobDownloadInfo download = await blobClient.DownloadAsync();
        
        using var reader = new StreamReader(download.Content);
        return await reader.ReadToEndAsync();
    }
    
    public async Task WriteBlob(string blobName, Stream data)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        
        await blobClient.UploadAsync(data);
    }
}