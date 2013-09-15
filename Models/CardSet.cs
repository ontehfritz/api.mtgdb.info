using System;
using MongoDB;
using MongoDB.Bson;

namespace Mtg.Model
{
    public class CardSet
    {
        public string Id { get; set; }

        public string name { get; set; }

        public string block { get; set; }

        public string description { get; set; }

        public string wikipedia { get; set; }

        public int common { get; set; }

        public int uncommon { get; set; }

        public int rare { get; set; }

        public int mythic_rare { get; set; }

        public int basic_land { get; set; }

        public DateTime released_at { get; set; }

        public int [] card_ids { get; set; }
    }
}

