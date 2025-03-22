namespace NPU.Data.DataHandlers;

public interface IBlobStorageDriver
{
    public  Task<string> ReadFileAsync(string blobName);
    public  Task WriteFileAsync(string path, string fileName, Stream data);
}