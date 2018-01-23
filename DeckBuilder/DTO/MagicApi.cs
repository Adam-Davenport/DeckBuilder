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
    public class MagicApi
    {
        private const string BASEURL = "https://api.magicthegathering.io/v1";   // Base url for mtg api
        private const string CARDS_REF = "cards";   // Card reference
        private const string SETS_REF = "sets";     // Set reference
        private const string PAGE_REF = "?page=";   // Page filter
        private int CardPage;                       // Used to keep track of pages for cards
        private int SetPage;                        // Track set page
        private DeckBuilderContext DbContext;       // Database context


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
			UpdateCards();
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
					try
					{
						Set NewSet = new Set(CurrentSet);
						DbContext.Set.Add(NewSet);
						DbContext.SaveChanges();
					}
					catch(Exception ex)
					{
					}
                }
				SetPage++;
				UpdateSets();
            }
        }

        public async void UpdateCards()
        {
            string Resource = CARDS_REF + PAGE_REF + CardPage.ToString();
            CardListDTO CardList = await GetDTOAsync<CardListDTO>(Resource);

            if(CardList.Cards.Count > 0)
            {
                foreach(CardDTO CurrentCard in CardList.Cards)
                {
					try
					{
						Card NewCard = new Card(CurrentCard);
						DbContext.Card.Add(NewCard);
					}
					catch(Exception e)
					{
						Console.WriteLine("There was an error adding a card");
					}
                }
                CardPage++;
                UpdateCards();
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
