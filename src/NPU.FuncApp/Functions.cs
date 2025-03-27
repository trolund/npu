using System.Reflection.Metadata;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NPU.Bl;
using NPU.Data.Config;

namespace NPU.FuncApp;

public class Functions(ILogger<Functions> logger, CosmosSettings cosmosSettings)
{
    private static string? _connection;
    
    [Function("CleanData")]
    public void Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer)
    {
        _connection = cosmosSettings.DB_CONNECTION_STRING;
    }
    
    [Function("CosmosTrigger")]
    public void Run([CosmosDBTrigger(
            databaseName: cosmosSettings,
            containerName:"TriggerItems",
            Connection = _connection,
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Document> scores,
        FunctionContext context)
    {
        if (scores is not null && scores.Any())
        {
            foreach (var doc in scores)
            {
                logger.LogInformation("score: {desc}", doc.Description);
            }
        }
    }
}