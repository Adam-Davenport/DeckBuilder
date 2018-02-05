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
		public static DeckBuilderContext DbContext { get; set;  }

		private static Dictionary<string, int> CardDictionary;

		public static List<Decklist> ParseDeckList(string raw, int id, DeckBuilderContext context)
		{
			CardDictionary = new Dictionary<string, int>();
			RawInput = raw;
			DeckId = id;
			DbContext = context;
			return GetDecklist();
		}

		public static List<Decklist> GetDecklist()
		{
			if(RawInput == null || RawInput == String.Empty)
			{
				return null;
			}
			else if(!RawInput.Contains("\r\n"))
			{
				List<Decklist> list = new List<Decklist>();
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
					if(CardDictionary.ContainsKey(dl.CardId))
					{
						CardDictionary[dl.CardId] += dl.Count;
					}
					else
					{
						CardDictionary.Add(dl.CardId, dl.Count);
					}
				}
			}
			return CompileList();
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
					string substring = line.Substring(0, index);
					count = int.Parse(substring);
					line = line.Substring(index + 1);
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

		private static List<Decklist> CompileList()
		{
			List<Decklist> list = new List<Decklist>();
			foreach(KeyValuePair<string, int> item in CardDictionary)
			{
				Decklist dl = new Decklist()
				{
					CardId = item.Key,
					Count = item.Value,
					DeckId = DeckId
				};
				list.Add(dl);
			}
			return list;
		}
	}
}
