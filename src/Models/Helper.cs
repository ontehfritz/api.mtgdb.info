using System;
using System.Collections.Generic;

namespace Mtg
{
    public class Helper
    {
        private static Dictionary<string,string> FieldType = 
            new Dictionary<string,string> () 
        {
            {"id","int"},
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
            {"formats","string[]"},
            {"releasedAt","DateTime"}
        };

        public static string GetCardFieldType(string field)
        {
            if (FieldType.ContainsKey (field)) 
            {
                return FieldType [field];
            }

            return null;
        }
    }
}

