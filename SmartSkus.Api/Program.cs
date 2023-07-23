using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSkus.Api;
using SmartSkus.Api.Data;
using System;

namespace SmartSkus.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // For Production
            // CreateHostBuilder(args).Build().Run();

            // For Devlopment
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        /// <summary>
        /// For Devlopment/Testing only, in prod we will deploy using migrations
        /// </summary>
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<InventoryContext>();

                    // To delete/reset database
                      context.Database.EnsureDeleted();

                    // To create Database scheama only
                    //context.Database.EnsureCreated();

                    // To create Scheams and Initialize Sample Data
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
