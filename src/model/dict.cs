using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;

namespace translate.dict.model.dict
{
    public class DictEntry
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("SourceLanguage")]
        public string SourceLanguage { get; set; }

        [BsonElement("SourceInput")]
        public string SourceInput { get; set; }

        [BsonElement("TargetLanguage")]
        public string TargetLanguage { get; set; }        

        [BsonElement("TargetResponses")]
        public List<DictResponse> TargetResponses { get; set; }

        public string ToReducedString() {
            return "Id="+ Id + ", SourceLanguage " + SourceLanguage + 
            ", TargetLanguage=" + TargetLanguage + ", SourceInput:" + SourceInput;
        }
    }

    public class DictResponse
    {
        public string DictSource { get; set; }

        public string SourcePhonetic { get; set; }
        
        public List<ResponseEntry> Entries { get; set; }

        public BsonDateTime CreatedOn { get; set; }

        public BsonDateTime ModifiedOn { get; set; }
    }

    public class ResponseEntry
    {
        public string SourceClassification { get; set; }

        public string TargetClassification { get; set; }

        public string TargetAnswer { get; set; }

        public string TargetPhonetic { get; set; }

        public BsonDateTime CreatedOn { get; set; }

        public BsonDateTime ModifiedOn { get; set; }
    }

    public class DummyObj
    {
        public DictEntry getDummy () {
            var dummy = new DictEntry();
            dummy.SourceInput = "123";
            dummy.SourceLanguage = "DE";
            dummy.TargetLanguage = "EN";

            dummy.TargetResponses = new List<DictResponse>();
            var singleResponse = new DictResponse();

            singleResponse.DictSource = "Hallo";
            singleResponse.CreatedOn = DateTime.Now;
            singleResponse.ModifiedOn = DateTime.Now;

            singleResponse.Entries = new List<ResponseEntry>();
            dummy.TargetResponses.Add(singleResponse);

            var singleResponseEntry = new ResponseEntry();
            singleResponse.Entries.Add(singleResponseEntry);

            singleResponseEntry.CreatedOn = DateTime.Now;
            singleResponseEntry.ModifiedOn = DateTime.Now;
            singleResponseEntry.SourceClassification = "Typical";
            singleResponseEntry.TargetClassification = "Common";
            singleResponseEntry.TargetAnswer = "Hello";

            return dummy;
        }
    }
}