using NUnit.Framework;
using System;
using MtgDb.Info;
using System.Collections.Generic;
using SuperSimple.Auth;
using MtgDbAdminDriver;
using Mtg;


namespace TestAdminDriver
{
    [TestFixture ()]
    public class Test
    {
        Admin admin;
        SuperSimpleAuth ssa;
        User ssaUser;
        IWriteRepository repository;
        string format = "yyyy-MM-dd";
        private const string connectionString = "mongodb://localhost";

        [SetUp()]
        public void Init()
        {
            repository = new MongoRepository (connectionString);
            admin =     new Admin("http://127.0.0.1:8082");
            ssa =       new SuperSimpleAuth ("testing_mtgdb.info", 
                "ae132e62-570f-4ffb-87cc-b9c087b09dfb");

            ssaUser =   ssa.Authenticate ("mtgdb_tester", 
                "test123", "127.0.0.1");
        }
           

        [Test ()]
        public void Test_update_card_rulings ()
        {
            List<Mtg.Model.Ruling> rulings = new List<Mtg.Model.Ruling> ();
            rulings.Add(new Mtg.Model.Ruling {ReleasedAt = DateTime.Now.ToString(format), 
                Rule = "test ruling 1"});
            rulings.Add(new Mtg.Model.Ruling {ReleasedAt = DateTime.Now.ToString(format), 
                Rule = "test ruling 2"});

            Mtg.Model.Card after = repository.UpdateCardRulings(1,rulings.ToArray()).Result;

            Assert.AreEqual (after.Rulings.Count, 2);
        }

        [Test ()]
        public void Test_add_card ()
        {
            Mtg.Model.Card card = new Mtg.Model.Card()
            {
                Id = -1,
                Name = "test",
                Description = "test",
                CardSetId = "10E",
                Formats = null,
                Rulings = null
            };
     
            card = repository.AddCard(card).Result;
            Assert.AreEqual(card.Id, -1);
        }


        [Test ()]
        public void Test_add_set ()
        {
            Mtg.Model.CardSet newSet = new Mtg.Model.CardSet
            {
                Id = "FRS",
                Name = "Fritz Set",
                Description = "Awesome sauce",
                //YYYY-MM-DD
                ReleasedAt = "2014-05-15",
                Block = "Fritz",
                Type = "Fritz",
                BasicLand = 0,
                Rare = 0, 
                MythicRare = 80,
                Common = 1,
                Uncommon = 1
            };

            newSet = repository.AddCardSet(newSet).Result;

            Assert.AreEqual(newSet.Id, "FRS");
        }

        [Test ()]
        public void Test_update_card ()
        {
            Mtg.Model.Card after = repository.UpdateCardField<string[]>(1, "colors", 
                new string[]{"blue","green"}).Result;
            //Card after = repository.UpdateCardField<string>(1, "flavor", "").Result;

            Assert.AreEqual (after.Colors[0], "blue");
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

            List<string> promos = new List<string>();
            promos.Add("FNM");

            card.Colors = colors.ToArray();
            card.Promos = promos.ToArray();

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

