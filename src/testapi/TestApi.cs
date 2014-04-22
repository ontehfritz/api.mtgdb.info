using NUnit.Framework;
using System;
using Mtg;
using Mtg.Model;
using Nancy;
using System.Collections.Generic;
using MongoDB.Driver;

namespace testapi
{
    //// xsp4 --port 8082
    /// run the above command in api.mtgdb.info
    [TestFixture ()]
    public class TestApi
    {
        private const string connectionString = "mongodb://localhost";
        string format = "yyyy-MM-dd";

        IRepository repository = new MongoRepository (connectionString);

        [Test()]
        public void Test_search_verify()
        {
            CardSearch search = new CardSearch("name eq 'shit and shit' and type not creature");
            string[] elements = search.Verify();
            foreach(string s in elements)
            {
                System.Console.WriteLine(s);
            }
        }

        [Test()]
        public void Test_mongo()
        {
            CardSearch search = new CardSearch("name eq 'Giant Growth'");
            List<IMongoQuery> queries = search.MongoQuery();
            Assert.Greater(queries.Count, 0);
        }

        [Test()]
        public void Test_complex_search()
        {
            Card [] cards = repository.Search("name eq 'Giant Growth'",true).Result;
            Assert.Greater(0, cards.Length);
        }

        [Test()]
        public void Test_get_card_by_setNumber()
        {
            Card card = repository.GetCardBySetNumber("THS", 90).Result;
            System.Console.Write(card.Id.ToString() + ":" + card.SetNumber.ToString());
            Assert.AreEqual("THS", card.CardSetId);
            Assert.AreEqual(90, card.SetNumber);
        }
            
        [Test()]
        public void Test_get_random_card()
        {
            Card rcard = repository.GetRandomCard().Result;
            System.Console.Write(rcard.Id.ToString());
            Assert.IsNotNull(rcard);
        }

        [Test()]
        public void Test_get_random_card_performance()
        {
            Card rcard = repository.GetRandomCard().Result;
           
            for(int i = 0; i < 1000; i++ )
            {
                rcard = repository.GetRandomCard().Result;
            }
        }
            
        [Test()]
        public void Test_get_random_card_in_set()
        {
            Card rcard = repository.GetRandomCardInSet("lea").Result;
            System.Console.Write(rcard.Id.ToString());
            Assert.IsNotNull(rcard);
        }
            
        [Test ()]
        public void Test_update_card_rulings ()
        {
            List<Ruling> rulings = new List<Ruling> ();
            rulings.Add(new Ruling {ReleasedAt = DateTime.Now.ToString(format), 
                Rule = "test ruling 1"});
            rulings.Add(new Ruling {ReleasedAt = DateTime.Now.ToString(format), 
                Rule = "test ruling 2"});

            Card after = repository.UpdateCardRulings(1,rulings.ToArray()).Result;
           
            Assert.AreEqual (after.Rulings.Count, 2);
        }

        [Test ()]
        public void Test_update_card ()
        {
            Card after = repository.UpdateCardField<string[]>(1, "colors", new string[]{"blue","green"}).Result;
            //Card after = repository.UpdateCardField<string>(1, "flavor", "").Result;

            Assert.AreEqual (after.Colors[0], "blue");
        }
            
        [Test ()]
        public void Test_get_multiple_sets ()
        {
            CardSet[] sets = repository.GetSets(new string[]{"all","arb"}).Result;
            Assert.Greater (sets.Length,1);
        }

        [Test ()]
        public void Test_get_multiple_cards ()
        {
            Card[] cards = repository.GetCards(new int[]{1,2,3,4,5}).Result;
            Assert.Greater (cards.Length,1);
        }

        [Test ()]
        public void Test_search_cards ()
        {
            Card[] cards = repository.Search ("giant").Result;
            Assert.Greater (cards.Length,1);
        }

        [Test ()]
        public void Test_get_cards_by_filter ()
        {
            DynamicDictionary query = new DynamicDictionary(); 

            query.Add ("name", "Ankh of Mishra");
            Card[] cards = repository.GetCards (query).Result;
            Assert.Greater (cards.Length,1);
        }

        [Test ()]
        public void Test_get_cards_by_set ()
        {
            Card[] cards = repository.GetCardsBySet ("10E").Result;
            Assert.Greater (cards.Length,1);
        }

        [Test ()]
        public void Test_get_cards_by_set_with_range ()
        {
            Card[] cards = repository.GetCardsBySet ("10E",11,20).Result;
            Assert.AreEqual (10, cards.Length);
        }


        [Test ()]
        public void Test_get_card()
        {
            Card card = repository.GetCard(1).Result;
            Assert.IsNotNull (card);
        }

        [Test ()]
        public void Test_get_cards_by_name ()
        {
            Card[] cards = repository.GetCards ("ankh of mishra").Result;
            Assert.Greater (cards.Length, 1);
        }

        [Test ()]
        public void Test_get_cards_by_name_without_match ()
        {
            Card[] cards = repository.GetCards ("").Result;
            Assert.Greater (cards.Length, 0);
        }

        [Test ()]
        public void Test_get_sets ()
        {
            CardSet[] sets = repository.GetSets().Result;
            Assert.Greater (sets.Length,1);
        }

        [Test ()]
        public void Test_get_set ()
        {
            CardSet set = repository.GetSet("10E").Result;
            Assert.IsNotNull (set);
        }
    }
}

