using System.Threading.Tasks;

namespace Mtg
{
    using System;
    using Mtg.Model;
    using Nancy;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using Nancy.ModelBinding;
    using Nancy.Json;
    using System.Dynamic;
    using System.Collections.Generic;

    public class MongoRepository : IRepository
    {
        private string Connection { get; set; }

        public async Task<Card[]> GetCards (dynamic query)
        {
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");
            var collection = database.GetCollection<Card> ("cards");

            MongoCursor<Card> cursor = null;                
            List<IMongoQuery> queries = new List<IMongoQuery> ();


            if (query.card_set_id != null) {
                queries.Add (Query<Card>.EQ (e => e.CardSetId, 
                                           (string)query.card_set_id));
            }

            if (query.artist != null) {
                queries.Add (Query<Card>.EQ (e => e.Artist, 
                                           (string)query.artist));
            }

            if (query.rarity != null) {
                queries.Add (Query<Card>.EQ (e => e.Rarity, 
                                           (string)query.rarity));
            }

            if (query.loyalty != null) {
                queries.Add (Query<Card>.EQ (e => e.Loyalty, 
                                           (int)query.loyalty));
            }

            if (query.loyalty != null) {
                queries.Add (Query<Card>.EQ (e => e.Loyalty, 
                                           (int)query.loyalty));
            }

            if (query.toughness != null) {
                queries.Add (Query<Card>.EQ (e => e.Toughness, 
                                           (int)query.toughness));
            }

            if (query.power != null) {
                queries.Add (Query<Card>.EQ (e => e.Power, 
                                           (int)query.power));
            }

            if (query.subtype != null) {
                queries.Add (Query<Card>.EQ (e => e.SubType, 
                                           (string)query.subtype));
            }


            if (query.card_set_name != null) {
                queries.Add (Query<Card>.EQ (e => e.CardSetName, 
                                           (string)query.card_set_name));
            }

            if (query.convertedmanacost != null) {
                queries.Add (Query<Card>.EQ (e => e.ConvertedManaCost, 
                                           (int)query.convertedmanacost));
            }

            if (query.card_set_number != null) {
                queries.Add (Query<Card>.EQ (e => e.SetNumber, 
                                           (int)query.card_set_number));
            }

            if (query.manacost != null) {
                queries.Add (Query<Card>.EQ (e => e.ManaCost, 
                                           (string)query.manacost));
            }

            if (query.colors != null) {
                foreach (string color in ((string)query.colors).ToString().Split(',')) {
                    queries.Add (Query<Card>.EQ (e => e.Colors, color));
                }
            }

            if (query.name != null) {
                queries.Add (Query<Card>.EQ (e => e.Name, 
                    (string)query.name));
            }

            if (query.type != null) {
                queries.Add (Query<Card>.EQ (e => e.Type, (string)query.type));
            }
            if (query.id != null) {
                queries.Add (Query<Card>.GT (e => e.Id, (int)query.id));
            }

            if (queries.Count > 0) {
                cursor = collection.Find (Query.And (queries)).SetSortOrder ("_id");
            } else {
                cursor = collection.FindAllAs<Card> ().SetSortOrder ("_id");
            }

            if (query.limit != null) {
                cursor.SetLimit ((int)query.limit);
            }

            List<Card> cards = new List<Card> ();

            foreach (Card card in cursor) {
                cards.Add (card);
            }

            return cards.ToArray ();
        }

        public async Task<Card[]> GetCardsBySet (string setId)
        {
            List<Card> cards = new List<Card> ();
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");

            var collection = database.GetCollection<Card> ("cards");
            var query = Query<Card>.EQ (e => e.CardSetId, (setId).ToUpper ());
            MongoCursor<Card> cursor = collection.Find (query).SetSortOrder ("setNumber");

            foreach (Card card in cursor) {
                cards.Add (card);
            }

            return cards.ToArray ();
        }

        public async Task<Card> GetCard (int id)
        {
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");

            var collection = database.GetCollection<Card> ("cards");
            var query = Query<Card>.EQ (e => e.Id, id);
            Card card = collection.FindOne (query);
            return card;
        }

        public async Task<CardSet[]> GetSets ()
        {
            List<CardSet> cardset = new List<CardSet> ();
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");

            var collection = database.GetCollection<CardSet> ("card_sets");
            MongoCursor<CardSet> cursor = collection.FindAllAs<CardSet> ()
                .SetSortOrder ("name");

            foreach (CardSet set in cursor) {
                cardset.Add (set);
            }

            return cardset.ToArray ();
        }

        public async Task<CardSet> GetSet (string id)
        {
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");
            var collection = database.GetCollection<CardSet> ("card_sets");
            var query = Query<CardSet>.EQ (e => e.Id, id.ToUpper ());
            CardSet cardset = collection.FindOne (query);
            return cardset;
        }

        public MongoRepository (string connection)
        {
            Connection = connection;
        }
    }
}

