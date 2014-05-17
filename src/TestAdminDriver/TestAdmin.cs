using NUnit.Framework;
using System;
using MtgDb.Info;
using System.Collections.Generic;
using SuperSimple.Auth;
using MtgDbAdminDriver;


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
        public void Test_Update_Token ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"token","true");

            Assert.IsTrue (updated);
        }


        [Test ()]
        public void Test_Add_New_Set ()
        {
            CardSet set = new CardSet(){
                Name = "Test Set",
                Description = "This is a test, I repeat this is a test.",
                Id = "TEST",
                Type = "TEST",
                Block = "TEST",
                Common = 0,
                Uncommon = 0,
                Rare = 0, 
                MythicRare = 0, 
                ReleasedAt = "2014-05-15" 
            };

            bool updated = admin.AddSet(ssaUser.AuthToken, set);
            Assert.IsTrue (updated);
        }


//        {"id","string"},
//        {"name", "string"},
//        {"description", "string"},
//        {"type", "string"},
//        {"common","int"},
//        {"uncommon","int"},
//        {"rare","int"},
//        {"mythicRare","int"},
//        {"basicLand","int"},
//        {"releasedAt","string"}//"yyyy-MM-dd"
        [Test ()]
        public void Test_Update_Set_Name  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","name","Test");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_Description  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","description","Test");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_Type  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","type","Test");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_Common  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","common","0");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_Uncommon  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","uncommon","0");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_Rare  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","rare","0");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_MythicRare  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","mythicRare","0");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_BasicLand  ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","basicLand","0");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Set_ReleasedAt ()
        {
            bool updated = admin.UpdateCardSetField(ssaUser.AuthToken,"THS","releasedAt",
                DateTime.Now.ToString("yyyy-MM-dd"));

            Assert.IsTrue (updated);
        }
            
        [Test ()]
        public void Test_Add_New_Card ()
        {
            Card card = new Card(){
                Id = -1,
                Name = "Test_Card",
                Description = "This is a test, I repeat this is a test.",
                CardSetId = "THS",
                Flavor = "Tis has the flav!",
                Type = "Creature",
                SubType = "Minion"
            };

            List<string> colors = new List<string>();
            colors.Add("black");
            colors.Add("blue");

            card.Colors = colors.ToArray();

            bool updated = admin.AddCard(ssaUser.AuthToken, card);

            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_RelatedCardId ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"relatedCardId","6");

            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_SetNumber ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"setNumber","6");

            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Name ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"name","Ankh of Mishra");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_SearchName ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,
                1,"searchName","ankhofmishra");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Description ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,
                "description","this is a test");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Flavor ()
        {
            bool updated = admin.UpdateCardField (ssaUser.AuthToken, 1, "flavor", "");
            Assert.IsTrue (updated);
        }
          
        [Test ()]
        public void Test_Update_Colors ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"colors","blue, red");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_ManaCost ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"manaCost","2");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_ConvertedManaCost ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"convertedManaCost","2");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_CardSetName  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"cardSetName","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_CardType  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"type","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_SubType  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"subType","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Power  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"power","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Toughness  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"toughness","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Loyalty  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"loyalty","1");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Rarity  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"rarity","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Artist  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"artist","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_CardSetId  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"cardSetId","update");
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Formats  ()
        {
            List<Format> formats = new List<Format> ();
            formats.Add (new Format(){ Name = "test 1", Legality = "Legal"});
            formats.Add (new Format(){ Name = "test 2", Legality = "Legal"});
            formats.Add (new Format(){ Name = "test 3", Legality = "Legal"});

            bool updated = admin.UpdateCardFormats(ssaUser.AuthToken,1,formats.ToArray());
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_ReleasedAt  ()
        {
            bool updated = admin.UpdateCardField(ssaUser.AuthToken,1,"releasedAt", 
                DateTime.Now.ToString("yyyy-MM-dd"));
            Assert.IsTrue (updated);
        }

        [Test ()]
        public void Test_Update_Rulings  ()
        {
            List<Ruling> rulings = new List<Ruling> ();
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now.ToString("yyyy-MM-dd"), Rule = "Rule 1"});
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now.ToString("yyyy-MM-dd"), Rule = "Rule 2"});
            rulings.Add (new Ruling(){ ReleasedAt = DateTime.Now.ToString("yyyy-MM-dd"), Rule = "Rule 3"});

            bool updated = admin.UpdateCardRulings(ssaUser.AuthToken,1,rulings.ToArray());
            Assert.IsTrue (updated);
        }
    }
}

