using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace NPU.Data.DataHandlers;

public class BlobStorageDriver()
{
    public async Task<string> ReadBlob(string releaseNoteFileName)
    {
        var blobName = $"release-notes/processed/v1/{releaseNoteFileName}.txt";

        // Create a BlobServiceClient object
        // TODO Connection string should be stored in a secure location
        var blobServiceClient = new BlobServiceClient("");

        // Get a container client
        // TODO Container name should be stored in a secure location
        var containerClient = blobServiceClient.GetBlobContainerClient("");

        // Get a blob client
        var blobClient = containerClient.GetBlobClient(blobName);
        
        // Download the blob's content
        BlobDownloadInfo download = await blobClient.DownloadAsync();

        // Read the blob's content as a string
        using var reader = new StreamReader(download.Content);
        return await reader.ReadToEndAsync();
    }
    
    public async Task WriteBlob(string model, string releaseNoteFileName, string data)
    {
        var blobName = $"result/model/{model}/v1/{releaseNoteFileName}.txt";

        // Create a BlobServiceClient object
        // TODO 
        var blobServiceClient = new BlobServiceClient("");

        // Get a container client
        // TODO 
        var containerClient = blobServiceClient.GetBlobContainerClient("");

        // Get a blob client
        var blobClient = containerClient.GetBlobClient(blobName);
        
        // Download the blob's content
        await blobClient.UploadAsync(data);
    }
}