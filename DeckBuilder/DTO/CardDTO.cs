using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DeckBuilder.DTO
{
    public class CardDTO
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "multiverseid")]
        public int MultiverseId { get; set; }

        [DataMember(Name = "manaCost")]
        public string ManaCost { get; set; }

        [DataMember(Name = "cmc")]
        public int Cmc { get; set; }

        [DataMember(Name = "colors")]
        public List<string>Colors { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "supertypes")]
        public string SuperTypes { get; set; }

        [DataMember(Name = "subtypes")]
        public string Subtypes { get; set; }

        [DataMember(Name = "rarity")]
        public string Rarity { get; set; }

        [DataMember(Name = "set")]
        public string SetCode { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "power")]
        public string Power { get; set; }

        [DataMember(Name = "toughness")]
        public string Toughness { get; set; }

        [DataMember(Name = "layout")]
        public string Layout { get; set; }

    }
}
