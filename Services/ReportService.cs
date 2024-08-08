using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CreditCardRewardPointsCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardRewardPointsCalculator.Services
{
    public class ReportService
    {
        private readonly RewardPointsDbContext _context;
        private readonly string _reportDirectory;

        public ReportService(RewardPointsDbContext context)
        {
            _context = context;

            // Set the absolute path for the reports directory
            _reportDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

            // Ensure the report directory exists
            if (!Directory.Exists(_reportDirectory))
            {
                Directory.CreateDirectory(_reportDirectory);
            }
        }

        public void GenerateReport(string reportType)
        {
            Console.WriteLine($"Generating report: {reportType}");
            try
            {
                switch (reportType.ToLower())
                {
                    case "last3months":
                        GenerateLast3MonthsTransactionsReport();
                        break;
                    case "rewardpoints":
                        GenerateRewardPointsPerUserReport(new DateTime(2023, 01, 01), new DateTime(2023, 12, 31));
                        break;
                    case "top5customers":
                        GenerateTop5CustomersReport(new DateTime(2023, 01, 01), new DateTime(2023, 12, 31));
                        break;
                    case "bottom5customers":
                        GenerateBottom5CustomersReport(new DateTime(2023, 01, 01), new DateTime(2023, 12, 31));
                        break;
                    default:
                        Console.WriteLine("Invalid report type specified.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
            }
        }

        private void GenerateLast3MonthsTransactionsReport()
        {
            Console.WriteLine("Generating Last 3 Months Transactions Report");
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            var transactions = _context.Transactions
                .Include(t => t.CreditCard)
                .ThenInclude(cc => cc.Customer)
                .Where(t => t.TransactionDate >= threeMonthsAgo)
                .OrderBy(t => t.TransactionDate)
                .ToList();

            var filePath = Path.Combine(_reportDirectory, "Last3MonthsTransactionsReport.txt");
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var transaction in transactions)
                    {
                        writer.WriteLine($"Customer: {transaction.CreditCard.Customer.Name}, " +
                                         $"Card Number: {transaction.CreditCard.CardNumber}, " +
                                         $"Transaction Date: {transaction.TransactionDate}, " +
                                         $"Amount: {transaction.Amount}, " +
                                         $"Reward Points: {transaction.RewardPoints}");
                    }
                }
                Console.WriteLine($"Report successfully generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file {filePath}: {ex.Message}");
            }
        }

        private void GenerateRewardPointsPerUserReport(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Generating Reward Points Per User Report");
            var transactions = _context.Transactions
                .Include(t => t.CreditCard)
                .ThenInclude(cc => cc.Customer)
                .Include(t => t.TransactionType)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .GroupBy(t => new { t.CreditCard.Customer.Name, t.TransactionType.TypeName })
                .Select(g => new
                {
                    CustomerName = g.Key.Name,
                    TransactionType = g.Key.TypeName,
                    TotalRewardPoints = g.Sum(t => t.RewardPoints)
                })
                .ToList();

            var filePath = Path.Combine(_reportDirectory, "RewardPointsPerUserReport.txt");
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var transaction in transactions)
                    {
                        writer.WriteLine($"Customer: {transaction.CustomerName}, " +
                                         $"Transaction Type: {transaction.TransactionType}, " +
                                         $"Total Reward Points: {transaction.TotalRewardPoints}");
                    }
                }
                Console.WriteLine($"Report successfully generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file {filePath}: {ex.Message}");
            }
        }

        private void GenerateTop5CustomersReport(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Generating Top 5 Customers Report");
            var customerTransactions = _context.Customers
                .SelectMany(c => c.CreditCards)
                .SelectMany(cc => cc.Transactions)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .Include(t => t.CreditCard)
                .ThenInclude(cc => cc.Customer)
                .ToList();

            var customers = customerTransactions
                .GroupBy(t => t.CreditCard.Customer.Name)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalRewardPoints = g.Sum(t => t.RewardPoints),
                    TotalSpend = g.Sum(t => t.Amount)
                })
                .OrderByDescending(c => c.TotalRewardPoints)
                .Take(5)
                .ToList();

            var filePath = Path.Combine(_reportDirectory, "Top5CustomersReport.txt");
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var customer in customers)
                    {
                        writer.WriteLine($"Customer: {customer.CustomerName}, " +
                                         $"Total Reward Points: {customer.TotalRewardPoints}, " +
                                         $"Total Spend: {customer.TotalSpend}");
                    }
                }
                Console.WriteLine($"Report successfully generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file {filePath}: {ex.Message}");
            }
        }

        private void GenerateBottom5CustomersReport(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Generating Bottom 5 Customers Report");
            var customerTransactions = _context.Customers
                .SelectMany(c => c.CreditCards)
                .SelectMany(cc => cc.Transactions)
                .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
                .Include(t => t.CreditCard)
                .ThenInclude(cc => cc.Customer)
                .ToList();

            var customers = customerTransactions
                .GroupBy(t => t.CreditCard.Customer.Name)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalRewardPoints = g.Sum(t => t.RewardPoints),
                    TotalSpend = g.Sum(t => t.Amount)
                })
                .OrderBy(c => c.TotalRewardPoints)
                .Take(5)
                .ToList();

            var filePath = Path.Combine(_reportDirectory, "Bottom5CustomersReport.txt");
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var customer in customers)
                    {
                        writer.WriteLine($"Customer: {customer.CustomerName}, " +
                                         $"Total Reward Points: {customer.TotalRewardPoints}, " +
                                         $"Total Spend: {customer.TotalSpend}");
                    }
                }
                Console.WriteLine($"Report successfully generated: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file {filePath}: {ex.Message}");
            }
        }
    }
}
