using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OneInc.AzFunc
{
    public static class EncodeString
    {
        [FunctionName("EncodeString")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string strToEncode = req.Query["stringtoencode"];

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                strToEncode = strToEncode ?? data?.stingtoencode;

                if (string.IsNullOrEmpty(strToEncode)) { throw new Exception("Parameter stringtoencode is required"); };


                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(strToEncode), Base64FormattingOptions.None);

                return new OkObjectResult(encodedStr);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
