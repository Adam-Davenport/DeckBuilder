using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckBuilder.Models
{
	public class Decklist
	{
		public int DeckId { get; set; }
		public string CardId { get; set; }
		public int Count { get; set; }
		public Deck Deck { get; set; }
		public Card Card { get; set; }


		public Decklist()
		{ }

	}
}
