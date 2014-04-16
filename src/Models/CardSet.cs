using System;
using MongoDB;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace Mtg.Model
{
    public class CardSet
    {
        [BsonId]
        [JsonProperty("id")]
        public string Id            { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name          { get; set; }
        [BsonElement("block")]
        [JsonProperty("block")]
        public string Block         { get; set; }
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type          { get; set; }
        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description   { get; set; }
        [BsonElement("common")]
        [JsonProperty("common")]
        public int Common           { get; set; }
        [BsonElement("uncommon")]
        [JsonProperty("uncommon")]
        public int Uncommon         { get; set; }
        [BsonElement("rare")]
        [JsonProperty("rare")]
        public int Rare             { get; set; }
        [BsonElement("mythicRare")]
        [JsonProperty("mythicRare")]
        public int MythicRare       { get; set; }
        [BsonElement("basicLand")]
        [JsonProperty("basicLand")]
        public int BasicLand        { get; set; }
        [BsonIgnore]
        [JsonProperty("total")]
        public int Total            
        {
            get 
            { 
                if(this.CardIds == null)
                {
                    return 0;
                }

                return this.CardIds.Length;
            } 
        }
        [BsonElement("releasedAt")]
        [JsonProperty("releasedAt")]
        public string ReleasedAt  { get; set; }
        [BsonElement("cardIds")]
        [JsonProperty("cardIds")]
        public int [] CardIds       { get; set; }
    }
}

