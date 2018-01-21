using System;
using System.Collections.Generic;

namespace DeckBuilder.Models
{
    class Deck
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public List<Card> Cards { get; set; }
    }
}