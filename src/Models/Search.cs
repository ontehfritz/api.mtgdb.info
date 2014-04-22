using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Mtg.Model;

namespace Mtg
{
    public class CardSearch
    {
        private Dictionary<string,string> fields = 
            new Dictionary<string, string>()
        {
            {"name",                "name"},
            {"description",         "description"},
            {"flavor",              "flavor"},
            {"color",               "colors"},
            {"manacost",            "manaCost"},
            {"convertedmanacost",   "convertedManaCost"},
            {"type",                "type"},
            {"subtype",             "subType"},
            {"power",               "power"},
            {"toughness",           "toughness"},
            {"loyalty",             "loyalty"},
            {"rarity",              "rarity"},
            {"artist",              "artist"},
            {"setId",               "cardSetId"}
        };
      
        private Dictionary<string,string> compareOperators = 
            new Dictionary<string, string>()
        {
            {"m" , "contains"},
            {"eq", "equal"},
            {"not","not"},
            {"gt" ,"greater than"},
            {"gte","greater than or equal"},
            {"lt", "less than"},
            {"lte","less than or equal"}
        };
       

        private Dictionary<string,string> operators = 
            new Dictionary<string, string>()
        {
            {"and" , "and"},
            {"or", "or"}
        };

        private string query;

        public CardSearch (string q)
        {
            query = q;
        }

        //[col][space][match type][space][value][space][And/Or][space] ...
        //example: color equals blue or color equals red
        public string[] Verify()
        {
            string [] queries = 
                query.Split('\'')
                .Select((element, index) => index % 2 == 0  
                    ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  
                    : new string[] { element })  
                    .SelectMany(element => element).ToArray();

            int i = 1; 
            foreach(string element in queries)
            {
                switch(i)
                {
                case 1: 
                    if(!fields.ContainsKey(element.ToLower()))
                    {
                        throw new Exception(element + ": is not a valid column");  
                    }
                    break;
                case 2: 
                    if(!compareOperators.ContainsKey(element.ToLower()))
                    {
                        throw new Exception(element + ": is not a valid compare operator");  
                    }
                    break;
                case 3:
                    break;
                case 4:
                    if(!operators.ContainsKey(element.ToLower()))
                    {
                        throw new Exception(element + ": is not a valid operator");  
                    }
                    break;
                }

                if(i == 4)
                {
                    i = 1;
                }
                else
                {
                    i++;
                }
            }
                
            return queries;
        }

        public List<IMongoQuery> MongoQuery()
        {
            List<IMongoQuery> queries = new List<IMongoQuery> ();
            string [] query = this.Verify();
            string col = null;
            string value = null;
            string method = null;
            string op = null;

            int queryCount = 
                (query.Length - 3) == 0 ? 1 : ((query.Length - 3) / 4) + 1;

            if(queryCount == 1)
            {
                col = query[0];
                method = query[1];
                value = query[2];

                queries.Add(this.Build(col,value,method));

                return queries;
            }
           
            return queries;
        }

        private IMongoQuery Build(string col, string value, 
                        string method, string op = null)
        {
            IMongoQuery query = null;
            switch(method)
            {
            case "eq":
                query = Query.EQ(col,value);
                break;  
            }

            return query;
        }
    }
}

