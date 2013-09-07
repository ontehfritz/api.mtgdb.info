namespace Mtg
{
	using System;
    using Nancy;
	using MongoDB.Driver;
	using MongoDB.Bson;
	using MongoDB;
	using MongoDB.Driver.Builders;
	using Nancy.ModelBinding;
	using Nancy.Json;
	using MongoDB.Driver.Linq;
	using Mtg.Model;
	using System.Dynamic;
	using System.Collections.Generic;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
			var connectionString = "mongodb://localhost";

			Get ["/"] = parameters => {
				return View ["index"];
			};

			Get ["/cards"] = parameters => {
				var client = new MongoClient(connectionString);
				var server = client.GetServer();
				var database = server.GetDatabase("mtg");
				var collection = database.GetCollection<Card>("cards");

				MongoCursor<Card> cursor = null;                
				List<IMongoQuery> queries = new List<IMongoQuery>();


				if(Request.Query.card_set_id != null){
					queries.Add(Query<Card>.EQ(e => e.card_set_id, 
					                           (string)Request.Query.card_set_id));
				}

				if(Request.Query.artist != null){
					queries.Add(Query<Card>.EQ(e => e.artist, 
					                           (string)Request.Query.artist));
				}

				if(Request.Query.rarity != null){
					queries.Add(Query<Card>.EQ(e => e.rarity, 
					                           (string)Request.Query.rarity));
				}

				if(Request.Query.loyalty != null){
					queries.Add(Query<Card>.EQ(e => e.loyalty, 
					                           (int)Request.Query.loyalty));
				}

				if(Request.Query.loyalty != null){
					queries.Add(Query<Card>.EQ(e => e.loyalty, 
					                           (int)Request.Query.loyalty));
				}

				if(Request.Query.toughness != null){
					queries.Add(Query<Card>.EQ(e => e.toughness, 
					                           (int)Request.Query.toughness));
				}

				if(Request.Query.power != null){
					queries.Add(Query<Card>.EQ(e => e.power, 
					                           (int)Request.Query.power));
				}

				if(Request.Query.subtype != null){
					queries.Add(Query<Card>.EQ(e => e.subtype, 
					                           (string)Request.Query.subtype));
				}


				if(Request.Query.card_set_name != null){
					queries.Add(Query<Card>.EQ(e => e.card_set_name, 
					                           (string)Request.Query.card_set_name));
				}

				if(Request.Query.convertedmanacost != null)
				{
					queries.Add(Query<Card>.EQ(e => e.convertedmanacost, 
					                           (int)Request.Query.convertedmanacost));
				}

				if(Request.Query.card_set_number != null)
				{
					queries.Add(Query<Card>.EQ(e => e.set_number, 
					                           (int)Request.Query.card_set_number));
				}

				if(Request.Query.manacost != null)
				{
					queries.Add(Query<Card>.EQ(e => e.manacost, 
					                           (string)Request.Query.manacost));
				}

				if(Request.Query.colors != null)
				{
					foreach(string color in ((string)Request.Query.colors).ToString().Split(','))
					{
						queries.Add(Query<Card>.EQ(e => e.colors, color));
					}
				}

				if(Request.Query.name != null)
				{
					queries.Add(Query<Card>.EQ(e => e.name, (string)Request.Query.name));
				}

				if(Request.Query.type != null)
				{
					queries.Add(Query<Card>.EQ(e => e.type, (string)Request.Query.type));
				}
				if(Request.Query.id != null)
				{
					queries.Add(Query<Card>.GT(e => e.Id, (int)Request.Query.id));
				}

				if(queries.Count > 0){
					cursor = collection.Find(Query.And(queries)).SetSortOrder("_id");
				}
				else
				{
					cursor = collection.FindAllAs<Card>().SetSortOrder("_id");
				}

				if(Request.Query.limit != null)
				{
					cursor.SetLimit((int)Request.Query.limit);
				}

				JsonSettings.MaxJsonLength = 100000000;
				return Response.AsJson(cursor);
			};


			Get["/cards/{id}"] = parameters => {
				var client = new MongoClient(connectionString);
				var server = client.GetServer();
				var database = server.GetDatabase("mtg");

				var collection = database.GetCollection<Card>("cards");
				var query = Query<Card>.EQ(e => e.Id, (int)parameters.id);
				Card card = collection.FindOne(query);
			
				return Response.AsJson(card);
            };

			Get ["/sets/{id}"] = parameters => {
				var client = new MongoClient(connectionString);
				var server = client.GetServer();
				var database = server.GetDatabase("mtg");
				var collection = database.GetCollection<CardSet>("card_sets");
				var query = Query<CardSet>.EQ(e => e.Id, ((string)parameters.id).ToUpper());
				CardSet cardSet = collection.FindOne(query);
				return Response.AsJson(cardSet);
			};

			Get ["/sets/"] = parameters => {
				var client = new MongoClient(connectionString);
				var server = client.GetServer();
				var database = server.GetDatabase("mtg");

				var collection = database.GetCollection<CardSet>("card_sets");
				MongoCursor<CardSet> cursor = collection.FindAllAs<CardSet>()
				.SetSortOrder("_id");

				JsonSettings.MaxJsonLength = 1000000;
				return Response.AsJson(cursor);
			};

			Get ["/sets/{id}/cards/"] = parameters => {
				var client = new MongoClient(connectionString);
				var server = client.GetServer();
				var database = server.GetDatabase("mtg");

				var collection = database.GetCollection<Card>("cards");
				var query = Query<Card>.EQ(e => e.card_set_id, ((string)parameters.id).ToUpper());
				MongoCursor<Card> cursor = collection.Find(query);
				//card.card_image = string.Format(_imageUrl,(int)parameters.id);

				JsonSettings.MaxJsonLength = 100000000;
				return Response.AsJson(cursor);
			};
        }
    }
}