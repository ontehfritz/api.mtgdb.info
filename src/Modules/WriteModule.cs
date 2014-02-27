using System;
using Nancy;
using Nancy.Security;

namespace Mtg
{
    public class WriteModule : NancyModule
    {
        private IRepository repository;

        public WriteModule (IRepository repository)
        {
            this.RequiresHttps();

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

            Put ["/cards/{id}"] = parameters => {
                UpdateCardModel model = new UpdateCardModel();
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
                    }
                }
                catch(Exception e)
                {
                    return Response.AsJson(false,
                        Nancy.HttpStatusCode.UnprocessableEntity);
                }
                    
                return Response.AsJson(true,
                    Nancy.HttpStatusCode.UnprocessableEntity);
            };
        }
    }
}

