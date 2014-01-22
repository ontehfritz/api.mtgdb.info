using System;
using System.Net;
using Newtonsoft.Json;
using MtgDb.Info;
using System.Text.RegularExpressions;

namespace MtgDb.Info.Driver
{
    public class Db
    {
        public string ApiUrl { get; set; }

        public Db ()
        {
            ApiUrl = "http://api.mtgdb.info";
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Mtgdb.Info.Wrapper.Db"/> class.
        /// Only use this method if you running a local version MtgDB api. 
        /// </summary>
        /// <param name="url">Custom Url if not using: "http://api.mtgdb.info";</param>
        public Db(string url)
        {
            ApiUrl = url;
        }

        public Card GetCard(int id)
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/cards/{1}", this.ApiUrl, id.ToString());
                var json = client.DownloadString(url);
                //var serializer = new JavaScriptSerializer();
                Card card = JsonConvert.DeserializeObject<Card>(json);

                return card;
            }
        }

        public Card[] GetCards(string name)
        {
            using (var client = new WebClient())
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                name = rgx.Replace(name, "");
                string url = string.Format ("{0}/cards/{1}", this.ApiUrl, name);
                var json = client.DownloadString(url);

                Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);

                return cards;
            }
        }

        public Card[] GetCards()
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/cards/", this.ApiUrl);
                var json = client.DownloadString(url);

                Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);

                return cards;
            }
        }

        public Card[] FilterCards(string property, string value)
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/cards/?{1}={2}", this.ApiUrl, property, value);
                var json = client.DownloadString(url);

                Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);

                return cards;
            }
        }

        public Card[] GetSetCards(string setId, int start = 0, int end = 0)
        {
            using (var client = new WebClient())
            {
                string url = null;
                if(start > 0 || end > 0)
                {
                    url = string.Format ("{0}/sets/{1}/cards/?start={2}&end={3}", 
                        this.ApiUrl, setId, start, end);
                }
                else
                {
                    url = string.Format ("{0}/sets/{1}/cards/", this.ApiUrl, setId);
                }
                 
                var json = client.DownloadString(url);

                Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);

                return cards;
            }
        }

        public CardSet GetSet(string setId)
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/sets/{1}", this.ApiUrl, setId);
                var json = client.DownloadString(url);

                CardSet set = JsonConvert.DeserializeObject<CardSet>(json);

                return set;
            }
        }

        public CardSet[] GetSets()
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/sets/", this.ApiUrl);
                var json = client.DownloadString(url);

                CardSet[] sets = JsonConvert.DeserializeObject<CardSet[]>(json);

                return sets;
            }
        }

        public Card[] Search(string text)
        {
            using (var client = new WebClient())
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                text = rgx.Replace(text, "");
                string url = string.Format ("{0}/search/{1}", this.ApiUrl, text);
                var json = client.DownloadString(url);

                Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);

                return cards;
            }
        }
    }
}

