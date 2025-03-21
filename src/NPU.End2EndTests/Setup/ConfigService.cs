using Microsoft.Extensions.Configuration;

namespace LockNote.End2EndTests.Setup;

public class ConfigService : IConfigService
{
    private readonly string _baseUrl;
    
    // load the configuration
    public ConfigService()
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Configuration file not found: {configPath}");
        }

        // Load appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensure you have the correct using directive
            .AddJsonFile(configPath, optional: false, reloadOnChange: true)
            .Build();

        // Read configuration values
        _baseUrl = config["BASEURL"];
    }
    
    public string GetBaseUrl()
    {
        return _baseUrl;
    }
}