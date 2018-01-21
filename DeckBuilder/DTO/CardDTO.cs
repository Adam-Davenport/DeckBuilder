using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DeckBuilder.DTO
{
    public class CardDTO
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        public string ManaCost { get; set; }
        public int Cmc { get; set; }
        public string Types { get; set; }
        public string SuperTypes { get; set; }
        public string Subtypes { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string Text { get; set; }
        
    }
}
