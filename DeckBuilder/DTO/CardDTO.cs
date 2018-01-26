using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace DeckBuilder.DTO
{
    public class CardDTO
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        
        [DeserializeAs(Name = "multiverseid")]
        public string MultiverseId { get; set; }

        [DeserializeAs(Name = "manaCost")]
        public string ManaCost { get; set; }

        [DeserializeAs(Name = "cmc")]
        public int Cmc { get; set; }

        [DeserializeAs(Name = "colors")]
        public List<string>Colors { get; set; }

        [DeserializeAs(Name = "type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "supertypes")]
        public List<string>SuperTypes { get; set; }

        [DeserializeAs(Name = "subtypes")]
        public List<String> Subtypes { get; set; }

        [DeserializeAs(Name = "rarity")]
        public string Rarity { get; set; }

        [DeserializeAs(Name = "set")]
        public string SetCode { get; set; }

        [DeserializeAs(Name = "text")]
        public string Text { get; set; }

        [DeserializeAs(Name = "imageUrl")]
        public string ImageUrl { get; set; }

        [DeserializeAs(Name = "artist")]
        public string Artist { get; set; }

        [DeserializeAs(Name = "power")]
        public string Power { get; set; }

        [DeserializeAs(Name = "toughness")]
        public string Toughness { get; set; }

        [DeserializeAs(Name = "layout")]
        public string Layout { get; set; }

        [DeserializeAs(Name = "number")]
		public string Number { get; set; }

	}
}
