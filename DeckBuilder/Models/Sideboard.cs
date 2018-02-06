using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBuilder.Models
{
    public class Sideboard
    {
		public int DeckId { get; set; }
		public string CardId { get; set; }
		public int Count { get; set; }
		public Deck Deck { get; set; }
		public Card Card { get; set; }

		public Sideboard()
		{ }
    }
}
