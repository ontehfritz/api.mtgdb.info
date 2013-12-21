using NUnit.Framework;
using System;
using Mtg;
using Mtg.Model;
using Nancy;

namespace testapi
{
    //// xsp4 --port 8082
    /// run the above command in api.mtgdb.info
    [TestFixture ()]
    public class TestApi
    {
        private const string connectionString = "mongodb://localhost";

        IRepository repository = new MongoRepository (connectionString);

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
            Assert.Greater (cards.Length,1);
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

