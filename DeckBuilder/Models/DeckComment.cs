using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeckBuilder.Models
{
    public class DeckComment
    {
		public int DeckId { get; set; }
		public int CommentId { get; set; }
		public string Author { get; set; }
		public string Text { get; set; }
		
		public DeckComment()
		{ }

	}
}
