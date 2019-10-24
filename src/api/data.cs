using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using translate.dict.model.dict;
using System.Collections.Generic;
using translate.dict.db;

namespace translate.dict.api
{
    public static class importAll
    {
        [FunctionName("importAll")]

        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            var entries = req.Query["entries"];

            try {
                DictDb.AddRange(MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<DictEntry>>(entries));
                return new OkResult();
            } catch {
                return new UnprocessableEntityObjectResult("Failed to parse");
            }

        }
    }

}