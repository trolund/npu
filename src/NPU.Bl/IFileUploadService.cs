namespace NPU.Bl;

public interface IFileUploadService
{
    Task<string> UploadFileAsync(string id, string fileName, Stream fileStream);
    Task<Stream> GetFileAsync(string id, string filename);
}