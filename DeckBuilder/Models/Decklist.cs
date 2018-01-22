using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBuilder.Models
{
    public class Decklist
    {
        public Deck Deck { get; set; }
        public Card Card { get; set; }
        public int Count { get; set; }
    }
}
