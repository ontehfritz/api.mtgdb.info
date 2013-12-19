using System;
using System.Runtime.Serialization;

namespace MtgDb.Info
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember(Name = "set_number")]
        public int SetNumber { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "flavor")]
        public string Flavor { get; set; }

        [DataMember(Name = "colors")]
        public string [] Colors { get; set; }

        [DataMember(Name = "manacost")]
        public string ManaCost { get; set; }

        [DataMember(Name = "convertedmanacost")]
        public int ConvertedManaCost { get; set; }

        [DataMember(Name = "card_set_name")]
        public string CardSetName { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "subtype")]
        public string SubType { get; set; }

        [DataMember(Name = "power")]
        public int Power { get; set; }

        [DataMember(Name = "toughness")]
        public int Toughness { get; set; }

        [DataMember(Name = "loyalty")]
        public int Loyalty { get; set; }

        [DataMember(Name = "rarity")]
        public string Rarity { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "card_image")]
        public string CardImage { get; set; }
       
        [DataMember(Name = "card_set_id")]
        public string CardSetId { get; set; }

        [DataMember(Name = "released_at")]
        public DateTime ReleasedAt { get; set; }
    }
}

