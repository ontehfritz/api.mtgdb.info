using System;
using NUnit.Framework;
using MtgDb.Info;
using MtgDb.Info.Driver;

namespace wrapper_test
{
    [TestFixture()]
    public class TestMtgDriver
    {
        [Test()]
        public void Test_get_card ()
        {
            Db mtginfo = new Db ();
            Card card = mtginfo.GetCard (106368);
            System.Console.WriteLine (card.ReleasedAt.ToLongDateString ());
            Assert.AreEqual (106368, card.Id);
        }
       
        [Test()]
        public void Test_get_cards_by_name ()
        {
            Db mtginfo = new Db ();
            Card [] cards = mtginfo.GetCards ("ankh of mishra");
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_cards ()
        {
            Db mtginfo = new Db ();
            Card [] cards = mtginfo.GetCards ();
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_set ()
        {
            Db mtginfo = new Db ();
            CardSet set = mtginfo.GetSet ("10E");
            System.Console.WriteLine (set.Id);
            Assert.AreEqual ("10E", set.Id);
        }

        [Test()]
        public void Test_get_set_cards ()
        {
            Db mtginfo = new Db ();
            Card[] cards = mtginfo.GetSetCards ("10E");
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_get_sets ()
        {
            Db mtginfo = new Db ();
            CardSet[] sets = mtginfo.GetSets();
            System.Console.WriteLine (sets.Length.ToString());
            Assert.GreaterOrEqual (sets.Length,1);
        }
    }
}

