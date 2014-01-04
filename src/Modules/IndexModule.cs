using GlynnTucker.Cache;
using System.Linq;
using System.Linq.Dynamic;

namespace Mtg
{
    using System;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Json;
    using Mtg.Model;
    using System.Dynamic;
    using System.Collections.Generic;

    public class IndexModule : NancyModule
    {
        private const string connectionString = "mongodb://localhost";
        private IRepository repo = new MongoRepository (connectionString);

        public IndexModule ()
        {
            Get ["/"] = parameters => {
                return View ["index"];
            };

            Get ["/cards", true] = async (parameters, ct) => {
                JsonSettings.MaxJsonLength = 100000000;

                Cache.AddContext("mtgdb"); 
                Card[] cards = null/*(Card[])Cache.Get("mtgdb", "all")*/;

                if(Cache.Contains("mtgdb", "all") && 
                   ((DynamicDictionary)Request.Query).Count == 0)
                {
                    cards = (Card[])Cache.Get("mtgdb", "all");
                }
                else
                {
                    cards = await repo.GetCards (Request.Query);
                    Cache.Add("mtgdb","all",cards);
                }


                if(Request.Query.Fields != null)
                {
                    var c = cards.AsQueryable().Select(string.Format("new ({0})",(string)Request.Query.Fields));
                    return Response.AsJson(c);
                }


                return Response.AsJson (cards);
            };

            Get ["/cards/{id}", true] = async (parameters, ct) => {
                int id = 0; 


                if(int.TryParse((string)parameters.id, out id))
                {
                    Card card = await repo.GetCard ((int)parameters.id);
                    return Response.AsJson (card);
                }
                else
                {
                    Card [] cards = await repo.GetCards ((string)parameters.id);
                    return Response.AsJson (cards);
                }
            };

            Get ["/sets/{id}", true] = async (parameters, ct) =>  {
                CardSet cardSet = await repo.GetSet ((string)parameters.id);
                return Response.AsJson (cardSet);
            };

            Get ["/sets/", true] = async (parameters, ct) => {
                CardSet[] cardset = await repo.GetSets ();
                JsonSettings.MaxJsonLength = 1000000;
                return Response.AsJson (cardset);
            };

            Get ["/sets/{id}/cards/", true] = async (parameters, ct) => {
                JsonSettings.MaxJsonLength = 100000000;
                int start = 0; 
                int end = 0; 

                if(Request.Query.start != null )
                {
                    int.TryParse((string)Request.Query.start, out start);
                }

                if(Request.Query.end != null )
                {
                    int.TryParse((string)Request.Query.end, out end);
                }

                Card[] cards = null;
                if(start > 0 || end > 0)
                {
                    cards =  await repo.GetCardsBySet ((string)parameters.id, start, end);
                }
                else
                {
                    cards = await repo.GetCardsBySet ((string)parameters.id);
                }

                if(Request.Query.Fields != null)
                {
                    var c = cards.AsQueryable().Select(string.Format("new ({0})",(string)Request.Query.Fields));
                    return Response.AsJson(c);
                }

                return Response.AsJson (cards);
            };
        }
    }
}