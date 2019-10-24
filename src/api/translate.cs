using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using translate.dict.db;

namespace translate.dict.api
{
    public static class Translator
    {
        private static readonly String MISSING_PARAMETER_TEXT = "Missing parameter ";

        [FunctionName("translate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            String sourceLanguage = req.Query["sourceLanguage"];
            if (sourceLanguage == null) {
                return new UnprocessableEntityObjectResult(MISSING_PARAMETER_TEXT + "sourceLanguage");
            }

            String targetLanguage = req.Query["targetLanguage"];
            if (targetLanguage == null) {
                return new UnprocessableEntityObjectResult(MISSING_PARAMETER_TEXT + "targetLanguage");
            }

            String payload = req.Query["payload"];
            if (payload == null) {
                return new UnprocessableEntityObjectResult(MISSING_PARAMETER_TEXT + "payload");
            }

            String requestorId = req.Query["requestorId"];
            if (requestorId == null) {
                return new UnprocessableEntityObjectResult(MISSING_PARAMETER_TEXT + "requestorId");
            } else if ( requestorId.Length > 255) {
                return new UnprocessableEntityObjectResult("Payload length above 255 chars");
            }

            // Lookup
            var result = await DictDb.FindTranslation(sourceLanguage, targetLanguage, payload);

            return result != null
                ? (ActionResult)new OkObjectResult(result)
                : new NotFoundObjectResult("Could not find " +
                    sourceLanguage + "->" + targetLanguage + ": " + payload);
        }


    }
}
