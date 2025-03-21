namespace NPU.Data.DataHandlers;

public static class FileWriter
{
    public static async void WriteFile(string filePath, string fileName, string content)
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

        await File.AppendAllTextAsync($"{filePath}/{fileName}", content);
    }
}