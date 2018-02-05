using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DeckBuilder.DTO;
using Microsoft.Extensions.Logging;

namespace DeckBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
			IWebHost host = BuildWebHost(args);
			PopulateDatabase(host);
			host.Run();
		}

		private static void PopulateDatabase(IWebHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				IServiceProvider Services = scope.ServiceProvider;
				try
				{
					DatabasePopulation API = new DatabasePopulation(Services);
					API.PopulateDatabase();
				}
				catch (Exception ex)
				{
					var logger = Services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}
		}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

}
