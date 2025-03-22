namespace NPU.Data.DataHandlers;

public interface IBlobStorageDriver
{
    public  Task<string> ReadFileAsync(string blobName);
    public  Task WriteFileAsync(string blobName, Stream data);
}