using System;
using NUnit.Framework;
using Mtgdb.Info;
using Mtgdb.Info.Wrapper;

namespace wrapper_test
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void Test_GetCard ()
        {
            Db mtginfo = new Db ();
            Card card = mtginfo.GetCard (106368);
            System.Console.WriteLine (card.ReleasedAt.ToLongDateString ());
            Assert.AreEqual (106368, card.Id);
        }

        [Test()]
        public void Test_GetCards ()
        {
            Db mtginfo = new Db ();
            Card [] cards = mtginfo.GetCards ();
            System.Console.WriteLine (cards.Length.ToString());
            Assert.GreaterOrEqual (cards.Length,1);
        }

        [Test()]
        public void Test_GetSet ()
        {
            Db mtginfo = new Db ();
            CardSet set = mtginfo.GetSet ("10E");
            System.Console.WriteLine (set.Id);
            Assert.AreEqual ("10E", set.Id);
        }
    }
}

