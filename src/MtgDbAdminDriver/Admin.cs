using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using MtgDbAdminDriver;


namespace MtgDb.Info
{
    /// <summary>
    /// This class is not intended to be used by the community. It is for admins to verify update to the Mtgdb.info 
    /// card database. You will need to be authenticated and be in the admin group to use these anyway. 
    /// </summary>
    public class Admin
    {
        private string _apiUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MtgDb.Info.Admin"/> class.
        /// </summary>
        public Admin ()
        {
            _apiUrl = "https://api.mtgdb.info";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtgDb.Info.Admin"/> class.
        /// </summary>
        /// <param name="url">custom url</param>
        public Admin(string url)
        {
            _apiUrl = url;
        }
       

        /// <summary>
        /// Updates the card field specified.
        /// </summary>
        /// <returns><c>true</c>, if card field was updated, <c>false</c> otherwise.</returns>
        /// <param name="authToken">Auth token, from SSA passed from mtgdb.info</param>
        /// <param name="mvid">Mvid.</param>
        /// <param name="field">Field.</param>
        /// <param name="value">Value.</param>
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
                reqparm.Add (string.Format ("ReleasedAt[{0}]", i), ruling.ReleasedAt);
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

        public bool UpdateCardFormats(Guid authToken, int mvid, Format[] formats)
        {
            bool end = false;

            System.Collections.Specialized.NameValueCollection reqparm = 
                new System.Collections.Specialized.NameValueCollection();


            reqparm.Add("AuthToken", authToken.ToString());

            int i = 0; 
            foreach(Format format in formats)
            {
                reqparm.Add (string.Format ("Name[{0}]", i), format.Name);
                reqparm.Add (string.Format ("Legality[{0}]", i), format.Legality);
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
                        client.UploadValues (string.Format ("{0}/cards/{1}/formats", _apiUrl, mvid), 
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

