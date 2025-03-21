namespace NPU.Data.DataHandlers;

public interface IBlobStorageDriver
{
    public  Task<string> ReadBlob(string blobName);
    public  Task WriteBlob(string blobName, Stream data);
}