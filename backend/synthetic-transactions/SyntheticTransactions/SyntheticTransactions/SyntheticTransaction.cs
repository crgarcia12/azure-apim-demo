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
        const string Version = "6";
        private static HttpClient httpClient = new HttpClient();


        [FunctionName(nameof(SyntheticTransaction))]
        public static async Task Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"{nameof(SyntheticTransaction)} trigger function v{Version} executed at: '{DateTime.Now}'");

            string endpoint = Environment.GetEnvironmentVariable("ApplicationEndpoint");

            for (int i = 0; i < 100; i++)
            {
                string response = await httpClient.GetStringAsync(endpoint);
                log.LogInformation($"{nameof(SyntheticTransaction)} responded '{endpoint}' trigger '{i}': '{response}'");
            }
        }
    }
}
