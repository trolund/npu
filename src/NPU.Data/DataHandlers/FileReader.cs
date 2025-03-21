namespace NPU.Data.DataHandlers;

public static class FileReader
{
    public static string ReadFile(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (FileNotFoundException e)
        {
            throw new FileNotFoundException($"File reader could not read the file {filePath}", e);
        }
    }
}