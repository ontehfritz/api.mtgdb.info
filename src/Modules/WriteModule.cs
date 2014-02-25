using System;
using Nancy;
using Nancy.Security;

namespace Mtg
{
    public class WriteModule : NancyModule
    {
        public WriteModule ()
        {
            this.RequiresHttps();

            Before += ctx => {
                try
                {
                    Guid.Parse(Request.Form["AuthToken"]);
                }
                catch(Exception e)
                {

                }

                return null;
            };

            Put ["/cards/{id}"] = parameters => {
                UpdateCardModel model = new UpdateCardModel();



                return HttpStatusCode.OK;
            };
        }
    }
}

