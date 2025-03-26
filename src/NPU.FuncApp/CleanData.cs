using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NPU.Bl;

namespace NPU.FuncApp;

public class CleanData(ILogger<CleanData> logger, NpuService npuService)
{
    [Function("CleanData")]
    public void Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer)
    {
        
    }
}