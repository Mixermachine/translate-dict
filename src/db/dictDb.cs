using System;
using System.Threading.Tasks;
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
            GetCollection().InsertMany(entries);
        }

        public static void RemoveRangeById(List<String> entries) {
            GetCollection().DeleteManyAsync(x => entries.Contains(x.Id.ToString()));
        }
    }
}


