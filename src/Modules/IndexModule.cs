using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using Mtg.Model;
using System.Configuration;
using System.Reflection;

namespace Mtg
{
    public class IndexModule : NancyModule
    {
        private IRepository repo;

        public IndexModule (IRepository repository)
        {
            repo = repository;

            Get ["/"] = parameters => 
            {
                return View ["index"];
            };

            Get ["/search/", true] = async (parameters, ct) => 
            {
                string query = (string)Request.Query.q;
                int limit = 0; 
                int start = 0; 

                if(Request.Query.limit != null)
                {
                    limit = (int)Request.Query.limit;
                }

                if(Request.Query.start != null)
                {
                    start = (int)Request.Query.start;
                }
                    
                Card [] cards = await repo.Search (query,start,limit,true);

                return Response.AsJson (cards);
            };


            Get ["/search/{text}", true] = async (parameters, ct) => 
            {
                int limit = 0; 
                int start = 0; 

                if(Request.Query.limit != null)
                {
                    limit = (int)Request.Query.limit;
                }

                if(Request.Query.start != null)
                {
                    start = (int)Request.Query.start;
                }

                Card [] cards = await repo.Search ((string)parameters.text,
                    start, limit, false);

                return Response.AsJson (cards);
            };

            Get ["/cards/random", true] = async (parameters, ct) => 
            {
                JsonSettings.MaxJsonLength =    100000000;
                Card card =                    null;
            
                card = await repo.GetRandomCard();

                return Response.AsJson (card);
            };

            Get ["/cards", true] = async (parameters, ct) => 
            {
                JsonSettings.MaxJsonLength =    100000000;
                Card[] cards =                  null;
               
                if(Request.Query.Fields != null)
                {
                    string[] fields = ((string)Request.Query.Fields).Split(',');

                    foreach(string field in fields)
                    {
                        if(typeof(Card).GetProperty(field,BindingFlags.IgnoreCase |  
                            BindingFlags.Public | BindingFlags.Instance) == null)
                        {
                            return Response.AsJson(string.Format("Field: {0} is invalid.", field),
                                HttpStatusCode.NotAcceptable);
                        }
                    }

                    cards = await repo.GetCards (Request.Query);

                    var c = cards
                            .AsQueryable()
                            .Select(string.Format("new ({0})",
                            (string)Request.Query.Fields));

                    return Response.AsJson(c);
                }
               
                cards = await repo.GetCards (Request.Query);
          
                return Response.AsJson (cards);
            };

            Get ["/cards/{id}", true] = async (parameters, ct) => 
            {
                try
                {
                    int[] multiverseIds = 
                        Array.ConvertAll(((string)parameters.id).Split(','), int.Parse);

                    if(multiverseIds.Length > 1)
                    {
                        Card [] cards = await repo.GetCards(multiverseIds);

                        return Response.AsJson(cards);
                    }
                }
                catch(Exception e)
                {
                    //swallo it, cannot convert parameter to int array 
                }


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

            Get ["/sets/{id}", true] = async (parameters, ct) =>  
            {
                string [] ids = ((string)parameters.id).Split(',');

                if(ids.Length > 1)
                {
                    CardSet[] cardSets = await repo.GetSets (ids);
                    return Response.AsJson (cardSets);
                }

                CardSet cardSet = await repo.GetSet ((string)parameters.id);
                return Response.AsJson (cardSet);
            };

            Get ["/sets/", true] = async (parameters, ct) => 
            {
                CardSet[] cardset =             await repo.GetSets ();
                JsonSettings.MaxJsonLength =    1000000;

                return Response.AsJson (cardset);
            };

            Get ["/sets/{id}/cards/random", true] = async (parameters, ct) => 
            {
                JsonSettings.MaxJsonLength =    100000000;
                Card card =                     null;

                card = await repo.GetRandomCardInSet((string)parameters.id);

                return Response.AsJson (card);
            };


            Get ["/sets/{id}/cards/{setNumber}", true] = async (parameters, ct) => 
            {
                Card cards = await repo.GetCardBySetNumber((string)parameters.id,
                    (int)parameters.setNumber);

                return Response.AsJson(cards);
            };

            Get ["/sets/{id}/cards/", true] = async (parameters, ct) => 
            {
                JsonSettings.MaxJsonLength =    100000000;
                int start =                     0; 
                int end =                       0; 

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
                    cards = await repo.GetCardsBySet ((string)parameters.id, start, end);
                }
                else
                {
                    cards = await repo.GetCardsBySet ((string)parameters.id);
                }

                if(Request.Query.Fields != null)
                {
                    var c = cards.AsQueryable()
                        .Select(string.Format("new ({0})",(string)Request.Query.Fields));

                    return Response.AsJson(c);
                }

                return Response.AsJson (cards);
            };
        }
    }
}