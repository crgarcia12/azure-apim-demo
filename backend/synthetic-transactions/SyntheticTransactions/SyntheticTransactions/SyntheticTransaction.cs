using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace SyntheticTransactions
{
    public static class SyntheticTransaction
    {
        [FunctionName(nameof(SyntheticTransaction))]
        public static async Task Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"{nameof(SyntheticTransaction)} trigger function v5 executed at: '{DateTime.Now}'");

            string endpoint = Environment.GetEnvironmentVariable("ApplicationEndpoint");

            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 100; i++)
                {
                    string response = await client.GetStringAsync(endpoint);
                    log.LogInformation($"{nameof(SyntheticTransaction)} responded '{endpoint}' trigger '{i}': '{response}'");
                }
            }


        }
    }
}
