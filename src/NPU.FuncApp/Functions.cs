using System.Reflection.Metadata;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NPU.FuncApp;

public class Functions(ILogger<Functions> logger)
{
    [Function("CosmosTrigger")]
    public void Run(
        [CosmosDBTrigger(
            databaseName: "NPU",
            containerName: "data",
            Connection = "CosmosDBConnection",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]
        IReadOnlyList<Document>? items,
        FunctionContext context)
    {
        if (items is null || items.Count == 0) return;
        logger.LogInformation("number of items: {ItemsCount}", items.Count);
    }
}