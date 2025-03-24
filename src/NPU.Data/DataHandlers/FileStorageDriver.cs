namespace NPU.Data.DataHandlers;

public class FileStorageDriver: IBlobStorageDriver
{
    public Task<Stream> ReadFileAsync(string filePath)
    {
        try
        {
            return Task.FromResult<Stream>(File.Open(filePath, FileMode.Open, FileAccess.Read));
        }
        catch (FileNotFoundException e)
        {
            throw new FileNotFoundException($"File reader could not read the file {filePath}", e);
        }
    }

    public async Task WriteFileAsync(string path, string fileName,  Stream data)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        await using var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.Write);
        await data.CopyToAsync(fileStream);
    }
}