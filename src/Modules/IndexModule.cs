﻿using GlynnTucker.Cache;

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

            Get ["/cards"] = parameters => {
                Cache.AddContext("mtgdb"); 
                Card[] cards = null/*(Card[])Cache.Get("mtgdb", "all")*/;

                if(Cache.Contains("mtgdb", "all") && 
                   ((DynamicDictionary)Request.Query).Count == 0)
                {
                    cards = (Card[])Cache.Get("mtgdb", "all");
                }
                else
                {
                    cards = repo.GetCards (Request.Query);
                    Cache.Add("mtgdb","all",cards);
                }

                JsonSettings.MaxJsonLength = 100000000;
                return Response.AsJson (cards);
            };

            Get ["/cards/{id}"] = parameters => {
                Card card = repo.GetCard ((int)parameters.id);
                return Response.AsJson (card);
            };

            Get ["/sets/{id}"] = parameters => {
                CardSet cardSet = repo.GetSet ((string)parameters.id);
                return Response.AsJson (cardSet);
            };

            Get ["/sets/"] = parameters => {
                CardSet[] cardset = repo.GetSets ();
                JsonSettings.MaxJsonLength = 1000000;
                return Response.AsJson (cardset);
            };

            Get ["/sets/{id}/cards/"] = parameters => {
                Card[] cards = repo.GetCardsBySet ((string)parameters.id);
                JsonSettings.MaxJsonLength = 100000000;
                return Response.AsJson (cards);
            };
        }
    }
}