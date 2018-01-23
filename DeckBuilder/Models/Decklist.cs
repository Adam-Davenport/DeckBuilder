using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckBuilder.Models
{
    public class Decklist
    {
        public int Deck { get; set; }
        public string Card { get; set; }
        public int Count { get; set; }
    }
}
