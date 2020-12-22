using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using StockPopularityCore.Services.Biznesradar;

namespace StockPopularityFunction
{
    public static class CheckStockPopularitiesTimer
    {


        [FunctionName("CheckStockPopularitiesTimer")]
        public static async Task RunAsync([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
            
        }


    }
}