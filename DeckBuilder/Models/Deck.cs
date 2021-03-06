using System;
using System.Collections.Generic;

namespace DeckBuilder.Models
{
    public class Deck
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; private set; }
		public ICollection<Decklist> Decklists { get; set; }

		public Deck()
		{
			Date = DateTime.Now;
		}
    }
}