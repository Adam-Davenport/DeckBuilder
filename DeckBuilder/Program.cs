using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DeckBuilder.Models;
using DeckBuilder.DTO;

namespace DeckBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MagicApi API = new MagicApi();
            API.UpdateDatabase();
            Console.Read();
            //SeedData.TestRestClient();
            //BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
