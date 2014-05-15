using System;

namespace MtgDbAdminDriver
{
    public class Ruling
    {
        public string ReleasedAt        { get; set; }
        public string Rule              { get; set; }
    }

    public class Format
    {
        public string Name              { get; set; }
        public string Legality          { get; set; }
    }

    public class Card
    {
        public int Id                   { get; set; }
        public int RelatedCardId        { get; set; }
        public int SetNumber            { get; set; }
        public string Name              { get; set; }
        public string Description       { get; set; }
        public string Flavor            { get; set; }
        public string [] Colors         { get; set; }
        public string ManaCost          { get; set; }
        public int ConvertedManaCost    { get; set; }
        public string Type              { get; set; }
        public string SubType           { get; set; }
        public int Power                { get; set; }
        public int Toughness            { get; set; }
        public int Loyalty              { get; set; }
        public string Rarity            { get; set; }
        public string Artist            { get; set; }
        public string CardSetId         { get; set; }
        public bool Token               { get; set; }
        public bool Promo               { get; set; }
    }

    public class CardSet
    {
        public string Id                { get; set; }
        public string Name              { get; set; }
        public string Block             { get; set; }
        public string Type              { get; set; }
        public string Description       { get; set; }
        public int Common               { get; set; }
        public int Uncommon             { get; set; }
        public int Rare                 { get; set; }
        public int MythicRare           { get; set; }
        public int BasicLand            { get; set; }
        public string ReleasedAt        { get; set; }
    }
}

