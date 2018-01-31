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
			DeckLists = GetDecklist();
			return DeckLists;
		}

		public static List<Decklist> GetDecklist()
		{
			List<Decklist> list = new List<Decklist>();
			if(RawInput == null || RawInput == String.Empty)
			{
				return null;
			}
			else if(!RawInput.Contains("\r\n"))
			{
				Decklist dl = ParseLine(RawInput);
				list.Add(dl);
				return list;
			}
			string[] strings = RawInput.Split("\r\n");
			foreach(string line in strings)
			{
				Decklist dl = ParseLine(line);
				if(dl != null)
				{
					list.Add(dl);
				}
			}
			return list;
		}

		private static Decklist ParseLine(string line)
		{
			line = line.Trim();
			int count = 1;
			int index = line.IndexOf(" ");
			if(index != -1)
			{
				try
				{
					count = int.Parse(line.Substring(0, index-1));
					line = line.Substring(index);
				}
				catch(FormatException)
				{ }
			}
			Card card = GetCard(line);
			if(card == null)
			{
				return null;
			}

			Decklist list = new Decklist()
			{
				CardId = card.Id,
				Count = count,
				DeckId = DeckId
			};
			return list;
		}

		private static Card GetCard(string cardString)
		{
			if(cardString != null && cardString != String.Empty)
			{
				// Attempt to find a card with the same name as the current line
				Card card = DbContext.Cards.First(c => c.Name == cardString);
				return card;
			}
			return null;
		}


	}
}
