using System;
using Nancy;
using Nancy.Security;
using Nancy.ModelBinding;

namespace Mtg
{
    public class WriteModule : NancyModule
    {
        private IRepository repository;

        public WriteModule (IRepository repository)
        {
            //this.RequiresHttps();

            this.repository = repository;

            Before += ctx => {
                try
                {
                    Guid.Parse(Request.Form["AuthToken"]);
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
                    //return Response.AsJson(false,
                    //Nancy.HttpStatusCode.UnprocessableEntity);
                }
                    
                return Response.AsJson(true,
                    Nancy.HttpStatusCode.OK);
            };
        }
    }
}

