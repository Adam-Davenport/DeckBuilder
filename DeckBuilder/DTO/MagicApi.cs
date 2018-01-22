using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using DeckBuilder.Models;

namespace DeckBuilder.DTO
{
    public class MagicApi
    {
        private const string BASEURL = "https://api.magicthegathering.io/v1";   // Base url for mtg api
        private const string CARDS_REF = "cards";   // Card reference
        private const string SETS_REF = "sets";     // Set reference
        private const string PAGE_REF = "?page=";   // Page filter
        private int CardPage;   // Used to keep track of pages for cards
        private int SetPage;    // Track set page


        public MagicApi()
        {
            CardPage = 1;   // Page starts at 1 and is not a 0 based index
            SetPage = 1;
        }

        // Call all update functions
        public void UpdateDatabase()
        {
            UpdateSets();
            UpdateCards();
        }

        // Update data for all sets
        public async void UpdateSets()
        {
            // Set our resource to sets and current page
            string Resource = SETS_REF + PAGE_REF + SetPage;
            SetListDTO SetList = await GetDTOAsync<SetListDTO>(Resource);

            // Check to make sure there are more sets
            if(SetList.Sets.Count > 0)
            {
                foreach(SetDTO Set in SetList.Sets)
                {
                    Console.WriteLine(Set.Code);
                }
            }
        }

        // Print all cards to console
        public async void UpdateCards()
        {
            // Set resource to cards at current page
            string Resource = CARDS_REF + PAGE_REF + CardPage.ToString();
            CardListDTO CardList = await GetDTOAsync<CardListDTO>(Resource);

            // Check to make sure there are cards
            if(CardList.Cards.Count > 0)
            {
                foreach(CardDTO Card in CardList.Cards)
                {
                    Console.WriteLine(Card.Name + ", " + Card.Colors);
                }
                Console.WriteLine(CardPage);
                CardPage++; // Continue our iteration
                UpdateCards();   // Recursively call this function
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
