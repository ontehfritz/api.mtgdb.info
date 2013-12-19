using MtgDb.Info.Driver;

namespace MtgDb.Info
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using MtgDb.Info;

    [DataContract]
    public class CardSet
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "block")]
        public string Block { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "wikipedia")]
        public string Wikipedia { get; set; }

        [DataMember(Name = "common")]
        public int Common { get; set; }

        [DataMember(Name = "uncommon")]
        public int Uncommon { get; set; }

        [DataMember(Name = "rare")]
        public int Rare { get; set; }

        [DataMember(Name = "mythic_rare")]
        public int MythicRare { get; set; }

        [DataMember(Name = "basic_land")]
        public int BasicLand { get; set; }

        [DataMember(Name = "released_at")]
        public DateTime ReleasedAt { get; set; }

        [DataMember(Name = "card_ids")]
        public int [] CardIds { get; set; }

        public Card[] GetCards()
        {
            Db db = new Db ();
            return db.GetSetCards (this.Id);
        }
    }
}

