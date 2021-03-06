﻿using System;
using System.Threading.Tasks;
using RestSharp;
using DeckBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeckBuilder.DTO
{
	/*
	 * Flow of update logic
	 * UpdateSets -> Update single set -> Update Boosters for set
	 * Update Cards -> Update Single card
	 */
    public class DatabasePopulation
    {
        private const string BASEURL = "https://api.magicthegathering.io/v1";   // Base url for mtg api
        private const string CARDS_REF = "cards";   // Card reference
        private const string SETS_REF = "sets";     // Set reference
        private const string PAGE_REF = "?page=";   // Page filter
        private int CardPage;                       // Used to keep track of pages for cards
        private int SetPage;                        // Track set page
        private DeckBuilderContext DbContext;       // Database context
		private const int MAX_PAGES = 5;


        public DatabasePopulation(IServiceProvider ServiceProvider)
        {
            DbContext = new DeckBuilderContext(ServiceProvider.GetRequiredService<DbContextOptions<DeckBuilderContext>>());
			// Page indexes are 1 based
            CardPage = 1;
            SetPage = 1;
        }

        // Call all update functions
        public async void PopulateDatabase()
        {
			if(!await DbContext.Decks.AnyAsync())
			{
				PopulateSets();
			}
		}

		public void SaveChanges()
		{
			try
			{
				DbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{ }
			catch (DbUpdateException)
			{ }
		}

        public async void PopulateSets()
        {
            string Resource = SETS_REF + PAGE_REF + SetPage;
            SetListDTO SetList = await GetDTOAsync<SetListDTO>(Resource);

            if(SetList != null && SetList.Sets.Count > 0)
            {
				Console.WriteLine("Set page: " + SetPage.ToString());
                foreach(SetDTO CurrentSet in SetList.Sets)
                {
					PopulateSet(CurrentSet);
                }
				SetPage++;
				PopulateSets();
            }
			else
			{
				PopulateCards();
			}
        }

		public void PopulateSet(SetDTO SetData)
		{
			Set NewSet = DbContext.Sets.Find(SetData.Code);
			if (NewSet == null)
			{
				NewSet = new Set(SetData);
				DbContext.Sets.Add(NewSet);
			}
			else
			{
				NewSet.UpdateFromDTO(SetData);
			}
			PopulateBooster(SetData);
		}

        public async void PopulateCards()
        {
            string Resource = CARDS_REF + PAGE_REF + CardPage.ToString();
            CardListDTO CardList = await GetDTOAsync<CardListDTO>(Resource);

            if(CardList != null && CardList.Cards.Count > 0)
            {
                foreach(CardDTO CurrentCard in CardList.Cards)
                {
					PopulateCard(CurrentCard);
                }
                CardPage++;
                PopulateCards();
            }
			else
			{
				SaveChanges();
			}
        }

		// Update a single card
		public void PopulateCard(CardDTO CardData)
		{
			// First check and see if card is already in the database
			Card CurrentCard = DbContext.Cards.Find(CardData.Id);
			if (CurrentCard == null)
			{
				Card NewCard = new Card(CardData);
				DbContext.Cards.Add(NewCard);
				PopulateCardColor(CardData);
			}
			else
			{
				//CurrentCard.UpdateFromDTO(CardData);
				//DbContext.Card.Update(CurrentCard);
			}
		}

		public void PopulateCardColor(CardDTO CardData)
		{
			if(CardData.Colors != null && CardData.Colors.Count > 0)
			{
				foreach(string color in CardData.Colors)
				{
					CardColor ExistingColor = DbContext.CardColors.Find(CardData.Id, color);
					if(ExistingColor == null)
					{
						CardColor cardColor = new CardColor
						{
							CardId = CardData.Id,
							Color = color
						};
						DbContext.CardColors.Add(cardColor);
					}
				}
			}
		}

		private void PopulateBooster(SetDTO CurrentSet)
		{
			if(CurrentSet.Booster != null && CurrentSet.Booster.Count > 0)
			{
				foreach(string CardType in CurrentSet.Booster)
				{
					Booster CurrentBooster = DbContext.Boosters.Find(CurrentSet.Code, CardType);
					if(CurrentBooster != null)
					{
						CurrentBooster = new Booster(CurrentSet.Code, CardType);
					}
				}
			}
		}

        // Async task to get data for a Data Transfer Object of any type
        public Task<T> GetDTOAsync<T>(String Resource) where T : new()
        {
            RestClient Client = GetRestClient();
            TaskCompletionSource<T> TaskCompletionSource = new TaskCompletionSource<T>();
            RestRequest Request = GetRestRequest(Resource);
            Client.BaseUrl = new Uri(BASEURL);
            Client.ExecuteAsync<T>(Request, (response) => TaskCompletionSource.SetResult(response.Data));
            return TaskCompletionSource.Task;
        }

        // Get rest client with base url of the api
        private RestClient GetRestClient()
        {
            RestClient Client = new RestClient();
            Client.BaseUrl = new Uri(BASEURL);
            return Client;
        }

        // Generate request with specified resource
        private RestRequest GetRestRequest(string Resource)
        {
            RestRequest Request = new RestRequest(Method.GET);
            Request.Resource = Resource;
            return Request;
        }
    }
}
