namespace NPU.Data.DataHandlers;

public interface IBlobStorageDriver
{
    public  Task<Stream> ReadFileAsync(string blobName);
    public  Task WriteFileAsync(string path, string fileName, Stream data);
}