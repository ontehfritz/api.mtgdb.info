using NUnit.Framework;
using System;
using MtgDb.Info;
using System.Collections.Generic;
using Mtg;
using SuperSimple.Auth;


namespace TestAdminDriver
{
    /*id                  Integer     : multiverse Id
    setNumber           Integer     : card number in the set
    name                String      : name of the card
    searchName          String      : easy to search card name
    description         String      : the cards actions
    flavor              String      : flavor text adds story, does not effect game
    colors              String[]    : colors of card
    manacost            String      : the description of mana to cast spell
    convertedManaCost   Integer     : the amount of mana needed to cast spell
    cardSetName         String      : the set or expansion the card belongs to
    type                String      : the type of card
    subType             String      : subtype of card
    power               Integer     : attack strength
    toughness           Integer     : defense strength 
    loyalty             Integer     : loyalty points usually on planeswalkers
    rarity              String      : the rarity of the card
    artist              String      : artist of the illustration
    cardSetId           String      : the abbreviated name of the set
    rulings             Ruling[]    : list of rulings for this card
    formats             String[]    : list of legal formats this card is in
    releasedAt          Date        : when the card was released*/

    [TestFixture ()]
    public class Test
    {
        Admin admin;
        SuperSimpleAuth ssa;
        User ssaUser;

        [SetUp()]
        public void Init()
        {
            admin =     new Admin("http://127.0.0.1:8082");
            ssa =       new SuperSimpleAuth ("testing_mtgdb.info", 
                "ae132e62-570f-4ffb-87cc-b9c087b09dfb");
            ssaUser =   ssa.Authenticate ("mtgdb_tester", 
                "test123", "127.0.0.1");
        }

        [Test ()]
        public void TestUpdateRelatedCardId ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"relatedCardId","6");
            //
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateSetNumber ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"setNumber","6");
//
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateName ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"name","Ankh of Mishra");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateSearchName ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"searchName","ankhofmishra");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateDescription ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"description","this is a test");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateFlavor ()
        {
            bool updated = admin.UpdateCardField (ssaUser.AuthToken, 1, "flavor", "");
            Assert.IsTrue (updated);
        }
          
        [Test ()]
        public void TestUpdateColors ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"colors","blue, red");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateManaCost ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"manaCost","2");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateConvertedManaCost ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"convertedManaCost","2");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateCardSetName  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"cardSetName","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateCardType  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"type","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateSubType  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"subType","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdatePower  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"power","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateToughness  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"toughness","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateLoyalty  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"loyalty","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateRarity  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"rarity","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateArtist  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"artist","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateCardSetId  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"cardSetId","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateFormats  ()
        {
            List<MtgDb.Info.Format> formats = new List<Format> ();
            formats.Add (new Format(){ Name = "test 1", Legality = "Legal"});
            formats.Add (new Format(){ Name = "test 2", Legality = "Legal"});
            formats.Add (new Format(){ Name = "test 3", Legality = "Legal"});

            bool updated = admin.UpdateCardFormats(ssaUser.AuthToken,1,formats.ToArray());
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateReleasedAt  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"releasedAt", DateTime.Now.ToString());
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void TestUpdateRulings  ()
        {
            List<MtgDb.Info.Ruling> rulings = new List<Ruling> ();
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now, Rule = "Rule 1"});
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now, Rule = "Rule 2"});
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now, Rule = "Rule 3"});

            bool updated = admin.UpdateCardRulings(ssaUser.AuthToken,1,rulings.ToArray());
            Assert.IsTrue (updated);
        }
    }
}

