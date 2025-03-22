namespace NPU.Data.DataHandlers;

public class FileStorageDriver: IBlobStorageDriver
{
    public Task<string> ReadFileAsync(string filePath)
    {
        try
        {
            return File.ReadAllTextAsync(filePath);
        }
        catch (FileNotFoundException e)
        {
            throw new FileNotFoundException($"File reader could not read the file {filePath}", e);
        }
    }

    public async Task WriteFileAsync(string filePath, Stream data)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            Console.WriteLine($"Directory '{filePath}' created successfully.");
        }
        else
        {
            Console.WriteLine($"Directory '{filePath}' already exists.");
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await data.CopyToAsync(fileStream);
    }
}