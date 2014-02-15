using System;
using System.Net;
using Newtonsoft.Json;
using MtgDb.Info;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MtgDb.Info.Driver
{
    public class Db
    {
        public string ApiUrl { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtgDb.Info.Driver.Db"/> class.
        /// </summary>
        public Db ()
        {
            ApiUrl = "http://api.mtgdb.info";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MtgDb.Info.Driver.Db"/> class.
        /// </summary>
        /// <param name="ssl">If set to <c>true</c> ssl.</param>
        public Db(bool ssl)
        {
            if(ssl)
            {
                ApiUrl = "https://api.mtgdb.info";
            }
            else
            {
                ApiUrl = "http://api.mtgdb.info";
            }
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

        /// <summary>
        /// Gets the card.
        /// </summary>
        /// <returns>The card.</returns>
        /// <param name="id">Identifier.</param>
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

        /// <summary>
        /// Gets the sets.
        /// </summary>
        /// <returns>The sets.</returns>
        /// <param name="setIds">Set identifiers.</param>
        public CardSet[] GetSets(string [] setIds)
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/sets/{1}", this.ApiUrl, 
                    string.Join(",", setIds));

                var json = client.DownloadString(url);
                List<CardSet> sets = new List<CardSet>();

                if(setIds.Length == 1)
                {
                    sets.Add (JsonConvert.DeserializeObject<CardSet>(json));
                }
                else
                {
                    sets.AddRange (JsonConvert.DeserializeObject<CardSet[]>(json));
                }

                return sets.ToArray();
            }
        }


        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <returns>The cards.</returns>
        /// <param name="multiverseIds">Multiverse identifiers.</param>
        public Card[] GetCards(int [] multiverseIds)
        {
            using (var client = new WebClient())
            {
                string url = string.Format ("{0}/cards/{1}", this.ApiUrl, 
                    string.Join(",", multiverseIds));

                var json = client.DownloadString(url);
                List<Card> cards = new List<Card>();

                if(multiverseIds.Length == 1)
                {
                    cards.Add (JsonConvert.DeserializeObject<Card>(json));
                }
                else
                {
                    cards.AddRange (JsonConvert.DeserializeObject<Card[]>(json));
                }

                return cards.ToArray();
            }
        }

        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <returns>The cards.</returns>
        /// <param name="name">Name.</param>
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

        /// <summary>
        /// Gets the cards.
        /// </summary>
        /// <returns>The cards.</returns>
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

        /// <summary>
        /// Filters the cards.
        /// </summary>
        /// <returns>The cards.</returns>
        /// <param name="property">Property.</param>
        /// <param name="value">Value.</param>
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

        /// <summary>
        /// Gets the set cards.
        /// </summary>
        /// <returns>The set cards.</returns>
        /// <param name="setId">Set identifier.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
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

        /// <summary>
        /// Gets the set.
        /// </summary>
        /// <returns>The set.</returns>
        /// <param name="setId">Set identifier.</param>
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

        /// <summary>
        /// Gets the sets.
        /// </summary>
        /// <returns>The sets.</returns>
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

        /// <summary>
        /// Search the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
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

