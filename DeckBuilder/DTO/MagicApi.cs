using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MagicApi
    {
        private const string BASEURL = "https://api.magicthegathering.io/v1";   // Base url for mtg api
        private const string CARDS_REF = "cards";   // Card reference
        private const string SETS_REF = "sets";     // Set reference
        private const string PAGE_REF = "?page=";   // Page filter
        private int CardPage;                       // Used to keep track of pages for cards
        private int SetPage;                        // Track set page
        private DeckBuilderContext DbContext;       // Database context
		private const int MAX_PAGES = 5;


        public MagicApi(IServiceProvider ServiceProvider)
        {
            // Get our db context
            DbContext = new DeckBuilderContext(ServiceProvider.GetRequiredService<DbContextOptions<DeckBuilderContext>>());
            CardPage = 1;   // Page starts at 1 and is not a 0 based index
            SetPage = 1;
        }

        // Call all update functions
        public void UpdateDatabase()
        {
            UpdateSets();
		}

		public void SaveChanges()
		{
			try
			{
				DbContext.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{ }
		}

        public async void UpdateSets()
        {
            string Resource = SETS_REF + PAGE_REF + SetPage;
            SetListDTO SetList = await GetDTOAsync<SetListDTO>(Resource);

            if(SetList != null && SetList.Sets.Count > 0)
            {
				Console.WriteLine("Set page: " + SetPage.ToString());
                foreach(SetDTO CurrentSet in SetList.Sets)
                {
					UpdateSet(CurrentSet);
                }
				SetPage++;
				UpdateSets();
            }
			else
			{
				UpdateCards();
			}
        }

		public void UpdateSet(SetDTO SetData)
		{
			Set NewSet = DbContext.Set.Find(SetData.Code);
			if (NewSet == null)
			{
				NewSet = new Set(SetData);
				DbContext.Set.Add(NewSet);
			}
			else
			{
				NewSet.UpdateFromDTO(SetData);
			}
			UpdateBooster(SetData);
		}

        public async void UpdateCards()
        {
            string Resource = CARDS_REF + PAGE_REF + CardPage.ToString();
            CardListDTO CardList = await GetDTOAsync<CardListDTO>(Resource);

            if(CardList != null && CardList.Cards.Count > 0 && CardPage < MAX_PAGES)
            {
                foreach(CardDTO CurrentCard in CardList.Cards)
                {
					UpdateCard(CurrentCard);
                }
                CardPage++;
                UpdateCards();
            }
			else
			{
				SaveChanges();
			}
        }

		// Update a single card
		public void UpdateCard(CardDTO CardData)
		{
			// First check and see if card is already in the database
			Card CurrentCard = DbContext.Card.Find(CardData.Id);
			if (CurrentCard == null)
			{
				Console.WriteLine("===============================");
				Console.WriteLine(CardData.Name + CardData.SetCode);
				Console.WriteLine("===============================");
				Card NewCard = new Card(CardData);
				DbContext.Card.Add(NewCard);
			}
			else
			{
				//CurrentCard.UpdateFromDTO(CardData);
				//DbContext.Card.Update(CurrentCard);
			}
		}

		private void UpdateBooster(SetDTO CurrentSet)
		{
			if(CurrentSet.Booster != null && CurrentSet.Booster.Count > 0)
			{
				foreach(string CardType in CurrentSet.Booster)
				{
					Booster CurrentBooster = DbContext.Booster.Find(CurrentSet.Code, CardType);
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
