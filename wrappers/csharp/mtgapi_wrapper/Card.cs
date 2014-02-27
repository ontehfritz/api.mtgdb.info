using System;
using System.Runtime.Serialization;

namespace MtgDb.Info
{
    [DataContract]
    public class Card
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "setNumber")]
        public int SetNumber { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "flavor")]
        public string Flavor { get; set; }

        [DataMember(Name = "colors")]
        public string [] Colors { get; set; }

        [DataMember(Name = "manaCost")]
        public string ManaCost { get; set; }

        [DataMember(Name = "convertedManaCost")]
        public int ConvertedManaCost { get; set; }

        [DataMember(Name = "cardSetName")]
        public string CardSetName { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "subType")]
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

        public string CardImage { 
            get {
                return string.Format("//api.mtgdb.info/content/card_images/{0}.jpeg", this.Id);
            }
        }

        public string ImageHiRes { 
            get {
                return string.Format("//api.mtgdb.info/content/hi_res_card_images/{0}.jpg", this.Id);
            }
        }
       
        [DataMember(Name = "cardSetId")]
        public string CardSetId { get; set; }

        [DataMember(Name = "rulings")]
        public Ruling[] Rulings { get; set; }

        [DataMember(Name ="formats")]
        public string [] Formats { get; set; }

        [DataMember(Name = "releasedAt")]
        public DateTime ReleasedAt { get; set; }
    }

    [DataContract]
    public class Ruling 
    {
        [DataMember(Name = "releasedAt")]
        public DateTime ReleasedAt { get; set; }
        [DataMember(Name = "rule")]
        public string Rule { get; set; }
    }
}

