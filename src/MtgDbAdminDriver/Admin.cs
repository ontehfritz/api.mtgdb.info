using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;


namespace MtgDb.Info
{
    public class Admin
    {
        private string _apiUrl;

        public Admin ()
        {
            _apiUrl = "https://api.mtgdb.info";
        }

        public Admin(string url)
        {
            _apiUrl = url;
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
                            "Post", reqparm);

                    responsebody = Encoding.UTF8.GetString(responsebytes);

                }
                catch(WebException e) 
                {
                    throw e;
                }

                end = JsonConvert.DeserializeObject<bool>(responsebody);
            }

            return end;
        }

        public bool UpdateCardRulings(Guid authToken, int mvid, Ruling[] rulings)
        {
            bool end = false;

            System.Collections.Specialized.NameValueCollection reqparm = 
                new System.Collections.Specialized.NameValueCollection();


            reqparm.Add("AuthToken", authToken.ToString());

            int i = 0; 
            foreach(Ruling ruling in rulings)
            {
                reqparm.Add (string.Format ("ReleasedAt[{0}]", i), ruling.ReleasedAt.ToString());
                reqparm.Add (string.Format ("Rule[{0}]", i), ruling.Rule);
                ++i;
            }

            string responsebody = "";

            using (WebClient client = new WebClient ()) 
            {
                try 
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate {
                        return true;
                    };

                    byte[] responsebytes = 
                        client.UploadValues (string.Format ("{0}/cards/{1}/rulings", _apiUrl, mvid), 
                            "Post", reqparm);

                    responsebody = Encoding.UTF8.GetString (responsebytes);

                } 
                catch (WebException e) 
                {
                    throw e;
                }

                end = JsonConvert.DeserializeObject<bool>(responsebody);
            }

            return end;
        }
    }
}

