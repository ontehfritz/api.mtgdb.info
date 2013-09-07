using System;
using MongoDB;
using MongoDB.Bson;

namespace Mtg.Model
{
	public class Card
	{
		private string _imageUrl = "http://api.mtgdb.info/Content/card_images/{0}.jpeg";
		public int Id { get; set; }
		public int set_number { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string flavor { get; set; }
		public string [] colors { get; set; }
		public string manacost { get; set; }
		public int convertedmanacost { get; set; }
		public string card_set_name { get; set; }
		public string type { get; set; }
		public string subtype { get; set; }
		public int power { get; set; }
		public int toughness { get; set; }
		public int loyalty { get; set; }
		public string rarity { get; set; }
		public string artist { get; set; }

		public string card_image { 
			get{
				return string.Format (_imageUrl, Id.ToString ());
			}
		}

		public string card_set_id { get; set; }
		public DateTime released_at { get; set; }
	}
}

