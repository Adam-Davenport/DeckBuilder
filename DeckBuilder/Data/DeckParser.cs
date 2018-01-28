using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckBuilder.Models;

namespace DeckBuilder.Data
{
    public class DeckParser
    {
		public static string RawInput { get; set; }
		public static int DeckId { get; set; }
		public static List<Decklist> DeckLists { get; set; }
		public static DeckBuilderContext DbContext { get; set;  }


		public static List<Decklist> ParseDeckList(string raw, int id, DeckBuilderContext context)
		{
			RawInput = raw;
			DeckId = id;
			DbContext = context;
			DeckLists = new List<Decklist>();
			foreach(string CID in GetCardIds())
			{
				Decklist item = new Decklist()
				{
					CardId = CID, DeckId = DeckId, Count = 1
				};
				DeckLists.Add(item);
			}
			return DeckLists;
		}

		public static List<string> GetCardIds()
		{
			List<string> CardIds = new List<string>();
			string[] strings = RawInput.Split("\r\n");
			foreach(string line in strings)
			{
				Console.WriteLine(line);
				if(line != null)
				{
					try
					{
						// Attempt to find a card with the same name as the current line
						Card card = DbContext.Cards.First(c => c.Name == line);
						if(card != null)
						{
							CardIds.Add(card.Id);
						}
					}
					catch(InvalidOperationException)
					{ }
				}
			}
			return CardIds;
		}
	}
}
