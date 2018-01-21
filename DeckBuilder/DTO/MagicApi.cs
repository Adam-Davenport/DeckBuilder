using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace DeckBuilder.DTO
{
    public class MagicApi
    {
        private const string BASEURL = "https://api.magicthegathering.io/v1";
        private const string CARDS_REF = "cards";
        private const string SETS_REF = "sets";
        private const string PAGE_REF = "?page=";
        private int CardPage;   // Used to keep track of pages for cards


        public MagicApi()
        {
            CardPage = 1;   // Page starts at 1 and is not a 0 based index
        }

        public void UpdateDatabase()
        {
            PrintCards();
        }

        // Print all cards to console
        public async void PrintCards()
        {
            string Resource = CARDS_REF + PAGE_REF + CardPage.ToString();
            CardListDTO CardList = await GetDTOAsync<CardListDTO>(CARDS_REF);

            // Check to make sure there are cards
            if(CardList.Cards.Count > 0)
            {
                foreach(CardDTO Card in CardList.Cards)
                {
                    Console.WriteLine(Card.Name);
                }
                Console.WriteLine(CardPage);
                CardPage++; // Continue our iteration
                PrintCards();
            }
        }

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
