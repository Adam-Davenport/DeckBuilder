using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckBuilder.Models;
using Faker;
using LoremNET;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeckBuilder.Data
{
	public class FakeData
	{
		private DeckBuilderContext DbContext;
		private List<Card> Cards;

		public FakeData(IServiceProvider ServiceProvider)
		{
			DbContext = new DeckBuilderContext(ServiceProvider.GetRequiredService<DbContextOptions<DeckBuilderContext>>());
			Cards = GetCards();
		}

		public void Generate()
		{
			if (!DbContext.Decks.Any())
			{
				FakeDecks();
			}
		}

		public void FakeDecks()
		{
			List<Deck> Decks = new List<Deck>();
			for (int i = 0; i < 100; i++)
			{
				Deck NewDeck = new Deck()
				{
					Name = CompanyFaker.Name(),
					ImageUrl = "https://picsum.photos/400/?random",
					Description = Lorem.Paragraph(5, 6, 4, 10),
					Author = NameFaker.FirstName()
				};
				Decks.Add(NewDeck);
			}
			try
			{
				DbContext.AddRange(Decks);
				DbContext.SaveChanges();
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex);
			}
			FakeDecklists(Decks);
		}

		public void FakeDecklists(List<Deck> Decks)
		{
			foreach (Deck deck in Decks)
			{
				try
				{
					List<Decklist> dl = GenerateDecklist(deck.Id);
					DbContext.DeckLists.AddRange(dl);
				}
				catch (System.Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			try
			{
				DbContext.SaveChanges();
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private List<Card> GetCards()
		{
			List<Card> CardList = DbContext.Cards.ToList();
			return CardList;
		}

		private List<Decklist> GenerateDecklist(int DeckId)
		{
			List<Decklist> Decklists = new List<Decklist>();
			int max = Cards.Count - 1;
			int index;
			Card NewCard;
			Dictionary<string, int> CardDictionary = new Dictionary<string, int>();
			for (int i = 0; i < 15; i++)
			{
				index = Lorem.Integer(0, max);
				NewCard = Cards[index];
				CardDictionary[NewCard.Id] = 4;
			}
			foreach(KeyValuePair<string, int> pair in CardDictionary)
			{
				Decklist NewDecklist = new Decklist()
				{
					DeckId = DeckId,
					Count = pair.Value,
					CardId = pair.Key
				};
				Decklists.Add(NewDecklist);
			}

			return Decklists;
		}
	}
}
