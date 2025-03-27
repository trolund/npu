using System.Reflection.Metadata;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NPU.Bl;
using NPU.Data.Config;

namespace NPU.FuncApp;

public class Functions(ILogger<Functions> logger, CosmosSettings cosmosSettings)
{
    [Function("CleanData")]
    public void Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer)
    {
        
    }
    
    [Function("CosmosTrigger")]
    public void Run(
        [CosmosDBTrigger(
            databaseName: "%CosmosDBDatabaseName%",  // Uses the value from App Settings
            containerName: "%CosmosDBContainerName%", // Uses the value from App Settings
            Connection = "CosmosDBConnection", // References the App Setting for connection string
            LeaseContainerName = "%LeaseContainerName%",  // Uses the value from App Settings
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Document>? todoItems,
        FunctionContext context)
    {
        if (todoItems == null || !todoItems.Any()) return;
        
        foreach (var doc in todoItems)
        {
            logger.LogInformation("hej");
        }
    }
}