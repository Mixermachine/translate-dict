using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using translate.dict.model.dict;
using System.Collections.Generic;

namespace translate.dict.db
{
    public static class DictDb
    {
        public static IMongoCollection<DictEntry> GetCollection() {
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBConnection"));
            var database = client.GetDatabase("translate");
            return database.GetCollection<DictEntry>("dict");
        }

        public static async Task<DictEntry> FindTranslation(String sourceLanguage, 
            String targetLanguage, String payload) {

            var collection = GetCollection();

            var result = collection.Find(x => x.SourceLanguage == sourceLanguage &&
                x.TargetLanguage == targetLanguage &&
                x.SourceInput == payload);
                        
            return await result.FirstAsync();
        }

        public static void AddRange(List<DictEntry> entries) {
            var collection = GetCollection();

            collection.InsertMany(entries);
        }        
    }    
}


