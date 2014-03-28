using System;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Configuration;

namespace Mtg.Model
{
    public class Card
    {
        [BsonId]
        [JsonProperty("id")]
        public int Id                   { get; set; }
        [JsonProperty("relatedCardId")]
        [BsonElement("relatedCardId")]
        public int RelatedCardId        { get; set; }
        [JsonProperty("setNumber")]
        [BsonElement("setNumber")]
        public int SetNumber            { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name              { get; set; }
        [BsonElement("searchName")]
        [JsonProperty("searchName")]
        public string SearchName        { get; set; }
        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description       { get; set; }
        [BsonElement("flavor")]
        [JsonProperty("flavor")]
        public string Flavor            { get; set; }
        [BsonElement("colors")]
        [JsonProperty("colors")]
        public string [] Colors         { get; set; }
        [BsonElement("manaCost")]
        [JsonProperty("manaCost")]
        public string ManaCost          { get; set; }
        [BsonElement("convertedManaCost")]
        [JsonProperty("convertedManaCost")]
        public int ConvertedManaCost    { get; set; }
        [BsonElement("cardSetName")]
        [JsonProperty("cardSetName")]
        public string CardSetName       { get; set; }
        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type              { get; set; }
        [BsonElement("subType")]
        [JsonProperty("subType")]
        public string SubType           { get; set; }
        [BsonElement("power")]
        [JsonProperty("power")]
        public int Power                { get; set; }
        [BsonElement("toughness")]
        [JsonProperty("toughness")]
        public int Toughness            { get; set; }
        [BsonElement("loyalty")]
        [JsonProperty("loyalty")]
        public int Loyalty              { get; set; }
        [BsonElement("rarity")]
        [JsonProperty("rarity")]
        public string Rarity            { get; set; }
        [BsonElement("artist")]
        [JsonProperty("artist")]
        public string Artist            { get; set; }
        [BsonElement("cardSetId")]
        [JsonProperty("cardSetId")]
        public string CardSetId         { get; set; }
        [BsonElement("rulings")]
        [JsonProperty("rulings")]
        public List<Ruling> Rulings     { get; set; }
        [BsonElement("formats")]
        [JsonProperty("formats")]
        public List<Format> Formats     { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("releasedAt")]
        [JsonProperty("releasedAt")]
        public DateTime ReleasedAt      { get; set; }
    }

    public class Ruling 
    {
        [BsonId]
        int Id                          { get; set;}
        [BsonElement("releasedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [JsonProperty("releasedAt")]
        public DateTime ReleasedAt      { get; set; }
        [BsonElement("rule")]
        [JsonProperty("rule")]
        public string Rule              { get; set; }
    }

    public class Format
    {
        [BsonId]
        int Id                          { get; set;}
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name              { get; set; }
        [BsonElement("legality")]
        [JsonProperty("legality")]
        public string Legality          { get; set; }
    }
}

