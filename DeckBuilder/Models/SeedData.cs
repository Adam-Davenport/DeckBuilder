using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System;
using DeckBuilder.DTO;
using RestSharp;

namespace DeckBuilder.Models
{
    class SeedData
    {

        public static void TestRestClient()
        {
            var Client = new RestClient();
            Client.BaseUrl = new Uri("https://api.magicthegathering.io/v1/cards");
            var Request = new RestRequest(Method.GET);
            //Request.Resource = "cards";
            var Response = Client.Execute<CardListDTO>(Request);
            Console.Write("Data: ");
            CardListDTO CardList = Response.Data;
            foreach(CardDTO Card in CardList.Cards)
            {
                Console.WriteLine(Card.Name);
            }
            Console.Read();
        }


    }
}