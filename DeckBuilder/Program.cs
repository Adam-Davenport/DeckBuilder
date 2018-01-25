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
			using (var scope = host.Services.CreateScope())
			{
				IServiceProvider Services = scope.ServiceProvider;

				try
				{
					MagicApi API = new MagicApi(Services);
					API.UpdateDatabase();
				}
				catch (Exception ex)
				{
					var logger = Services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}
			host.Run();

		}

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
