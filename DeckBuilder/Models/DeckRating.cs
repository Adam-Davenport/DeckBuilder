using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBuilder.Models
{
    public class DeckRating
    {
		public int DeckId { get; set; }
		public string Author { get; set; }
		public int Rating { get; set; }

		public DeckRating()
		{ }
	}
}
