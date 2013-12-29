using System;
using NUnit.Framework;
using MtgDb.Info;
using MtgDb.Info.Driver;

namespace wrapper_test
{
    [TestFixture()]
    public class TestMtgDriver
    {
        Db mtginfo = new Db("http://127.0.0.1:8082");

        [Test()]
        public void Test_get_card ()
        {
            Card card = mtginfo.GetCard (106368);
            System.Console.WriteLine (card.ReleasedAt.ToLongDateString ());
            Assert.AreEqual (106368, card.Id);
        }
       
        [Test()]
        public void Test_get_cards_by_name ()
        {
            Card [] cards = mtginfo.GetCards ("ankh of mishra");
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_cards ()
        {
            Card [] cards = mtginfo.GetCards ();
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_set ()
        {
            CardSet set = mtginfo.GetSet ("10E");
            System.Console.WriteLine (set.Id);
            Assert.AreEqual ("10E", set.Id);
        }

        [Test()]
        public void Test_get_set_cards ()
        {
            Card[] cards = mtginfo.GetSetCards ("10E");
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_set_cards_with_range ()
        {
            Card[] cards = mtginfo.GetSetCards ("10E",1,10);
            System.Console.WriteLine (cards.Length.ToString());
            Assert.AreEqual (cards.Length,10);
        }

        [Test()]
        public void Test_get_sets ()
        {
            CardSet[] sets = mtginfo.GetSets();
            System.Console.WriteLine (sets.Length.ToString());
            Assert.GreaterOrEqual (sets.Length,1);
        }
    }
}

