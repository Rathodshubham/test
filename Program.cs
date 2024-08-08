using System;
using CreditCardRewardPointsCalculator.Models;
using CreditCardRewardPointsCalculator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreditCardRewardPointsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<RewardPointsDbContext>();
                context.Database.Migrate(); // Apply migrations

                // Initialize and seed the database
                DbInitializer.Initialize(context);

                // Calculate reward points
                var rewardPointsService = new RewardPointsService(context);
                rewardPointsService.CalculateRewardPoints();

                // Generate reports
                var reportService = new ReportService(context);

                // Prompt user for report type
                Console.WriteLine("Select the type of report to generate:");
                Console.WriteLine("1 - Last 3 months transactions per user per card");
                Console.WriteLine("2 - Reward points per user per transaction type for any given duration");
                Console.WriteLine("3 - Top 5 customers in any given duration with their reward points and spend");
                Console.WriteLine("4 - Bottom 5 customers in any given duration with their reward points and spend");
                Console.Write("Enter the number of your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        reportService.GenerateReport("last3months");
                        break;
                    case "2":
                        reportService.GenerateReport("rewardpoints");
                        break;
                    case "3":
                        reportService.GenerateReport("top5customers");
                        break;
                    case "4":
                        reportService.GenerateReport("bottom5customers");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No report generated.");
                        break;
                }
            }

            Console.WriteLine("Credit Card Reward Points Calculator");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureServices((context, services) =>
          {
              services.AddDbContext<RewardPointsDbContext>(options =>
                  options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CreditCardRewardPointsCal;Trusted_Connection=True;"));
          });
    }
}
