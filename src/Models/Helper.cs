using System;
using System.Collections.Generic;

namespace Mtg
{
    public class Helper
    {
        private static Dictionary<string,string> FieldType = 
            new Dictionary<string,string> () 
        {
            {"id","string"},
            {"name", "string"},
            {"description", "string"},
            {"type", "string"},
            {"common","int"},
            {"uncommon","int"},
            {"toughness","int"},
            {"rare","int"},
            {"mythicRare","int"},
            {"basicLand","int"},
            {"releasedAt","string"}//"yyyy-MM-dd"
        };


        private static Dictionary<string,string> SetFieldType = 
            new Dictionary<string,string> () 
        {
            {"id","int"},
            {"relatedCardId","int"},
            {"setNumber", "int"},
            {"name", "string"},
            {"searchName", "string"},
            {"description", "string"},
            {"flavor", "string"},
            {"colors", "string[]"},
            {"manaCost", "string"},
            {"convertedManaCost", "int"},
            {"cardSetName", "string"},
            {"type", "string"},
            {"subType","string"},
            {"power","int"},
            {"toughness","int"},
            {"loyalty","int"},
            {"rarity","string"},
            {"artist","string"},
            {"cardSetId","string"},
            {"token","bool"},
            {"formats","string[]"},
            {"releasedAt","string"}//"yyyy-MM-dd"
        };

        public static string GetCardFieldType(string field)
        {
            if (FieldType.ContainsKey (field)) 
            {
                return FieldType [field];
            }

            return null;
        }

        public static string GetSetFieldType(string field)
        {
            if (SetFieldType.ContainsKey (field)) 
            {
                return SetFieldType [field];
            }

            return null;
        }
           
    }
}

