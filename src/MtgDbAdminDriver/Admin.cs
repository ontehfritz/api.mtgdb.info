using System;
using System.Net;
using System.Text;


namespace MtgDb.Info
{
    public class Admin
    {
        private string _apiUrl;

        public Admin ()
        {
            _apiUrl = "https://api.mtgdb.info";
        }
            
        public bool UpdateCardField(Guid authToken, int mvid, string field, string value)
        {
            bool end = false;

            using(WebClient client = new WebClient())
            {
                System.Collections.Specialized.NameValueCollection reqparm = 
                    new System.Collections.Specialized.NameValueCollection();

               
                reqparm.Add("AuthToken", authToken.ToString());
                reqparm.Add("Field", field);
                reqparm.Add("Value", value);

                string responsebody = "";

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    byte[] responsebytes = 
                        client.UploadValues(string.Format("{0}/cards/{1}",_apiUrl, mvid), 
                            "Put", reqparm);

                    responsebody = Encoding.UTF8.GetString(responsebytes);

                }
                catch(WebException e) 
                {
                    return false;
                }

                //end = JsonConvert.DeserializeObject<bool>(responsebody);
            }

            return end;
        }
    }
}

