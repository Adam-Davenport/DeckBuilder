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
			FakeDecks();

		}

		public void FakeDecks()
		{
			List<Deck> Decks = new List<Deck>();
			for (int i = 0; i < 2000; i++)
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
			catch (Exception)
			{

			}
			FakeDecklists(Decks);
		}

		public void FakeDecklists(List<Deck> Decks)
		{
			foreach (Deck deck in Decks)
			{
				try
				{
					DbContext.DeckLists.AddRange(GenerateDecklist(deck.Id));
				}
				catch (System.Exception ex)
				{
					Console.WriteLine(ex);
				}
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
			for (int i = 0; i < 15; i++)
			{
				index = Lorem.Integer(0, max);
				NewCard = Cards[index];
				Decklist NewDecklist = new Decklist()
				{
					DeckId = DeckId,
					Count = 4,
					CardId = NewCard.Id
				};
				Decklists.Add(NewDecklist);
			}
			return Decklists;
		}
	}
}
