using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace sensors_api
{
    public static class Temperature
    {
        public const string FunctionName = "Temperature";

        [FunctionName(FunctionName)]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{FunctionName} called");

            int returnObj = 18;

            log.LogInformation($"{FunctionName} returned '{returnObj}'");
            return new OkObjectResult(returnObj);
        }
    }
}
