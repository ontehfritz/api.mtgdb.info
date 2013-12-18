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

        public Card[] GetCards (dynamic query)
        {
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");
            var collection = database.GetCollection<Card> ("cards");

            MongoCursor<Card> cursor = null;                
            List<IMongoQuery> queries = new List<IMongoQuery> ();


            if (query.card_set_id != null) {
                queries.Add (Query<Card>.EQ (e => e.card_set_id, 
                                           (string)query.card_set_id));
            }

            if (query.artist != null) {
                queries.Add (Query<Card>.EQ (e => e.artist, 
                                           (string)query.artist));
            }

            if (query.rarity != null) {
                queries.Add (Query<Card>.EQ (e => e.rarity, 
                                           (string)query.rarity));
            }

            if (query.loyalty != null) {
                queries.Add (Query<Card>.EQ (e => e.loyalty, 
                                           (int)query.loyalty));
            }

            if (query.loyalty != null) {
                queries.Add (Query<Card>.EQ (e => e.loyalty, 
                                           (int)query.loyalty));
            }

            if (query.toughness != null) {
                queries.Add (Query<Card>.EQ (e => e.toughness, 
                                           (int)query.toughness));
            }

            if (query.power != null) {
                queries.Add (Query<Card>.EQ (e => e.power, 
                                           (int)query.power));
            }

            if (query.subtype != null) {
                queries.Add (Query<Card>.EQ (e => e.subtype, 
                                           (string)query.subtype));
            }


            if (query.card_set_name != null) {
                queries.Add (Query<Card>.EQ (e => e.card_set_name, 
                                           (string)query.card_set_name));
            }

            if (query.convertedmanacost != null) {
                queries.Add (Query<Card>.EQ (e => e.convertedmanacost, 
                                           (int)query.convertedmanacost));
            }

            if (query.card_set_number != null) {
                queries.Add (Query<Card>.EQ (e => e.set_number, 
                                           (int)query.card_set_number));
            }

            if (query.manacost != null) {
                queries.Add (Query<Card>.EQ (e => e.manacost, 
                                           (string)query.manacost));
            }

            if (query.colors != null) {
                foreach (string color in ((string)query.colors).ToString().Split(',')) {
                    queries.Add (Query<Card>.EQ (e => e.colors, color));
                }
            }

            if (query.name != null) {
                queries.Add (Query<Card>.EQ (e => e.name, (string)query.name));
            }

            if (query.type != null) {
                queries.Add (Query<Card>.EQ (e => e.type, (string)query.type));
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

        public Card[] GetCardsBySet (string setId)
        {
            List<Card> cards = new List<Card> ();
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");

            var collection = database.GetCollection<Card> ("cards");
            var query = Query<Card>.EQ (e => e.card_set_id, (setId).ToUpper ());
            MongoCursor<Card> cursor = collection.Find (query).SetSortOrder ("set_number");

            foreach (Card card in cursor) {
                cards.Add (card);
            }

            return cards.ToArray ();
        }

        public Card GetCard (int id)
        {
            var client = new MongoClient (Connection);
            var server = client.GetServer ();
            var database = server.GetDatabase ("mtg");

            var collection = database.GetCollection<Card> ("cards");
            var query = Query<Card>.EQ (e => e.Id, id);
            Card card = collection.FindOne (query);
            return card;
        }

        public CardSet[] GetSets ()
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

        public CardSet GetSet (string id)
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

