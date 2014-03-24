using System;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Mtg.Model;
using SuperSimple.Auth;
using System.Configuration;

namespace Mtg
{
    public class WriteModule : NancyModule
    {
        private IRepository repository;
        private SuperSimpleAuth ssa; 
       
        public WriteModule (IRepository repository)
        {
            //this.RequiresHttps();

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

            Post ["/cards/{id}"] = parameters => {
                UpdateCardModel model = this.Bind<UpdateCardModel>();
                int mvid = (int)parameters.id;

                try
                {
                    switch(Helper.GetCardFieldType(model.Field))
                    {
                    case "int":
                        repository.UpdateCardField<int>(mvid,model.Field,int.Parse(model.Value));
                        break;
                    case "string":
                        repository.UpdateCardField<string>(mvid,model.Field, model.Value);
                        break;
                    case "string[]":
                        repository.UpdateCardField<string[]>(mvid,model.Field, model.Value.Split(','));
                        break;
                    case "DateTime":
                        repository.UpdateCardField<DateTime>(mvid,model.Field, DateTime.Parse(model.Value));
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

            Post ["/cards/{id}/rulings"] = parameters => {
                List<Ruling> rulings = this.Bind<List<Ruling>>();
                int mvid = (int)parameters.id;

                try
                {
                    repository.UpdateCardRulings(mvid,rulings.ToArray());
                }
                catch(Exception e)
                {
                    throw e;
                }

                return Response.AsJson("true");
            };

            Post ["/cards/{id}/formats"] = parameters => {
                List<Format> formats = this.Bind<List<Format>>();
                int mvid = (int)parameters.id;

                try
                {
                    repository.UpdateCardFormats(mvid,formats.ToArray());
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

