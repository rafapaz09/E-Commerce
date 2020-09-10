using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace PaymentFunction
{
    public static class Payment
    {
        [FunctionName("PaymentFunction")]
        public static void Run([QueueTrigger("payment", Connection = "AzureWebJobsStorage")]
            string myQueueItem, 
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function for queueItem: {myQueueItem}");
            log.LogInformation($"C# Doing some work.....");
            log.LogInformation($"C# Finishing some work at: {DateTime.Now}");
        }
    }
}
