using System;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Mtg.Model;
using SuperSimple.Auth;
using System.Configuration;
using System.Linq;

namespace Mtg
{
    public class WriteModule : NancyModule
    {
        private IRepository repository;
        private SuperSimpleAuth ssa; 
       
        public WriteModule (IRepository repository)
        {
            if(bool.Parse(ConfigurationManager.AppSettings.Get ("https")))
            {
                this.RequiresHttps();
            }
                
            this.repository = repository;
            ssa = new SuperSimpleAuth (ConfigurationManager.AppSettings.Get ("domain"),
                ConfigurationManager.AppSettings.Get ("key"));

            Before += ctx => {
                try
                {
                    Guid authKey = Guid.Parse(Request.Form["AuthToken"]);
                    User user = ssa.Validate(authKey,this.Context.Request.UserHostAddress);

                    if(user == null || 
                        !user.InRole("admin"))
                    {
                        return HttpStatusCode.Forbidden;
                    }
                }
                catch(Exception e)
                {
                    return HttpStatusCode.ProxyAuthenticationRequired;
                }

                return null;
            };

            Post["/sets", true] = async(parameters, ct) => {
                CardSet model =    this.Bind<CardSet>();
                CardSet set =     await repository.AddCardSet(model);

                if(set == null)
                {
                    return Response.AsJson("false",
                        Nancy.HttpStatusCode.UnprocessableEntity);
                }

                //return Response.AsJson(card);
                return Response.AsJson(true);
            };

            Post ["/sets/{id}", true] = async(parameters, ct) => {
                UpdateCardModel model = this.Bind<UpdateCardModel>();
                string setId = (string)parameters.id;

                try
                {
                    switch(Helper.GetSetFieldType(model.Field))
                    {
                    case "bool":
                        await repository.UpdateCardSet<bool>(setId, model.Field,
                            bool.Parse(model.Value ?? "false"));
                        break;
                    case "int":
                        await repository.UpdateCardSet<int>(setId, model.Field,
                            int.Parse(model.Value ?? "0"));
                        break;
                    case "string":
                        await repository.UpdateCardSet<string>(setId, model.Field, 
                            model.Value ?? "");
                        break;
                    case "string[]":
                        await repository.UpdateCardSet<string[]>(setId, model.Field, 
                            model.Value.Split(','));
                        break;
                    case "DateTime":
                        await repository.UpdateCardSet<DateTime>(setId, model.Field, 
                            DateTime.Parse(model.Value));
                        break;
                    default:
                        return Response.AsJson("false",
                            Nancy.HttpStatusCode.UnprocessableEntity);
                    }
                }
                catch(Exception e)
                {
                    throw e;
                    //                    return Response.AsJson(false,
                    //                        Nancy.HttpStatusCode.UnprocessableEntity);
                }

                return Response.AsJson(true,
                    Nancy.HttpStatusCode.OK);
            };


            Post["/cards", true] = async(parameters, ct) => {
                Card model =    this.Bind<Card>();
                Card card =     await repository.AddCard(model);

                if(card == null)
                {
                    return Response.AsJson("false",
                        Nancy.HttpStatusCode.UnprocessableEntity);
                }

                //return Response.AsJson(card);
                return Response.AsJson(true);
            };

            Post ["/cards/{id}", true] = async(parameters, ct) => {
                UpdateCardModel model = this.Bind<UpdateCardModel>();
                int mvid = (int)parameters.id;

                try
                {
                    switch(Helper.GetCardFieldType(model.Field))
                    {
                    case "bool":
                        await repository.UpdateCardField<bool>(mvid,model.Field,
                            bool.Parse(model.Value ?? "false"));
                        break;
                    case "int":
                        await repository.UpdateCardField<int>(mvid,model.Field,
                            int.Parse(model.Value ?? "0"));
                        break;
                    case "string":
                        await repository.UpdateCardField<string>(mvid,model.Field, 
                            model.Value ?? "");
                        break;
                    case "string[]":
                        await repository.UpdateCardField<string[]>(mvid,model.Field, 
                            model.Value.Split(','));
                        break;
                    case "DateTime":
                        await repository.UpdateCardField<DateTime>(mvid,model.Field, 
                            DateTime.Parse(model.Value));
                        break;
                    default:
                        return Response.AsJson("false",
                            Nancy.HttpStatusCode.UnprocessableEntity);
                    }
                }
                catch(Exception e)
                {
                    throw e;
//                    return Response.AsJson(false,
//                        Nancy.HttpStatusCode.UnprocessableEntity);
                }
                    
                return Response.AsJson(true,
                    Nancy.HttpStatusCode.OK);
            };

            Post ["/cards/{id}/rulings", true] = async(parameters, ct) => {
                List<Ruling> rulings = this.Bind<List<Ruling>>();
                int mvid = (int)parameters.id;

                try
                {
                    await repository.UpdateCardRulings(mvid,rulings.ToArray());
                }
                catch(Exception e)
                {
                    throw e;
                }

                return Response.AsJson("true");
            };

            Post ["/cards/{id}/formats", true] = async(parameters, ct) => {
                List<Format> formats = this.Bind<List<Format>>();
                int mvid = (int)parameters.id;

                try
                {
                    await repository.UpdateCardFormats(mvid,formats.ToArray());
                }
                catch(Exception e)
                {
                    throw e;
                }

                return Response.AsJson("true");
            };
        }
    }
}

