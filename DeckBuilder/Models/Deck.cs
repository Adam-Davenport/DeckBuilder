using System;
using System.Collections.Generic;

namespace DeckBuilder.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public List<Card> Cards { get; set; }
    }
}