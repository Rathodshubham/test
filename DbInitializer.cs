using System;
using System.Linq;
using CreditCardRewardPointsCalculator.Models;

namespace CreditCardRewardPointsCalculator
{
    public static class DbInitializer
    {
        public static void Initialize(RewardPointsDbContext context)
        {
            // Check if the database is already seeded.
            if (context.Customers.Any())
            {
                return; // Database has been seeded.
            }

            // Seed Customers
            var customers = new Customer[]
            {
                new Customer { Name = "Amit Sharma", Email = "amit.sharma@example.com", PhoneNumber = "9876543210" },
                new Customer { Name = "Priya Singh", Email = "priya.singh@example.com", PhoneNumber = "9876543211" },
                new Customer { Name = "Ravi Kumar", Email = "ravi.kumar@example.com", PhoneNumber = "9876543212" },
                new Customer { Name = "Anjali Patel", Email = "anjali.patel@example.com", PhoneNumber = "9876543213" },
                new Customer { Name = "Vijay Desai", Email = "vijay.desai@example.com", PhoneNumber = "9876543214" },
                new Customer { Name = "Neha Verma", Email = "neha.verma@example.com", PhoneNumber = "9876543215" },
                new Customer { Name = "Rahul Mehta", Email = "rahul.mehta@example.com", PhoneNumber = "9876543216" },
                new Customer { Name = "Kavita Gupta", Email = "kavita.gupta@example.com", PhoneNumber = "9876543217" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Seed CreditCards
            var creditCards = new CreditCard[]
            {
                new CreditCard { CardNumber = "1111222233334444", ExpirationDate = new DateTime(2025, 12, 31), CreditLimit = 50000, CustomerId = customers[0].CustomerId },
                new CreditCard { CardNumber = "5555666677778888", ExpirationDate = new DateTime(2026, 12, 31), CreditLimit = 75000, CustomerId = customers[1].CustomerId },
                new CreditCard { CardNumber = "9999000011112222", ExpirationDate = new DateTime(2027, 12, 31), CreditLimit = 30000, CustomerId = customers[2].CustomerId },
                new CreditCard { CardNumber = "3333444455556666", ExpirationDate = new DateTime(2028, 12, 31), CreditLimit = 45000, CustomerId = customers[3].CustomerId },
                new CreditCard { CardNumber = "7777888899990000", ExpirationDate = new DateTime(2029, 12, 31), CreditLimit = 60000, CustomerId = customers[4].CustomerId },
                new CreditCard { CardNumber = "2222333344445555", ExpirationDate = new DateTime(2025, 06, 30), CreditLimit = 40000, CustomerId = customers[5].CustomerId },
                new CreditCard { CardNumber = "6666777788889999", ExpirationDate = new DateTime(2026, 07, 31), CreditLimit = 55000, CustomerId = customers[6].CustomerId },
                new CreditCard { CardNumber = "0000111122223333", ExpirationDate = new DateTime(2027, 08, 31), CreditLimit = 35000, CustomerId = customers[7].CustomerId }
            };
            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();

            // Seed TransactionTypes
            var transactionTypes = new TransactionType[]
            {
                new TransactionType { TypeName = "Retail" },
                new TransactionType { TypeName = "Groceries" },
                new TransactionType { TypeName = "Utilities" },
                new TransactionType { TypeName = "Travel" }
            };
            context.TransactionTypes.AddRange(transactionTypes);
            context.SaveChanges();

            // Seed RewardPointRanges
            var rewardPointRanges = new RewardPointRange[]
            {
                new RewardPointRange { TransactionTypeId = transactionTypes[0].TransactionTypeId, MinAmount = 0, MaxAmount = 1000, Points = 1 },
                new RewardPointRange { TransactionTypeId = transactionTypes[0].TransactionTypeId, MinAmount = 1000, MaxAmount = 5000, Points = 5 },
                new RewardPointRange { TransactionTypeId = transactionTypes[1].TransactionTypeId, MinAmount = 0, MaxAmount = 500, Points = 2 },
                new RewardPointRange { TransactionTypeId = transactionTypes[1].TransactionTypeId, MinAmount = 500, MaxAmount = 2000, Points = 8 },
                new RewardPointRange { TransactionTypeId = transactionTypes[2].TransactionTypeId, MinAmount = 0, MaxAmount = 1000, Points = 3 },
                new RewardPointRange { TransactionTypeId = transactionTypes[2].TransactionTypeId, MinAmount = 1000, MaxAmount = 5000, Points = 10 },
                new RewardPointRange { TransactionTypeId = transactionTypes[3].TransactionTypeId, MinAmount = 0, MaxAmount = 2000, Points = 4 },
                new RewardPointRange { TransactionTypeId = transactionTypes[3].TransactionTypeId, MinAmount = 2000, MaxAmount = 10000, Points = 15 }
            };
            context.RewardPointRanges.AddRange(rewardPointRanges);
            context.SaveChanges();

            // Seed Transactions
            var transactions = new Transaction[]
            {
                new Transaction { CreditCardId = creditCards[0].CreditCardId, TransactionTypeId = transactionTypes[0].TransactionTypeId, TransactionDate = new DateTime(2023, 01, 15, 10, 30, 0), Amount = 1200, RewardPoints = rewardPointRanges[1].Points },
                new Transaction { CreditCardId = creditCards[0].CreditCardId, TransactionTypeId = transactionTypes[1].TransactionTypeId, TransactionDate = new DateTime(2023, 02, 20, 15, 45, 0), Amount = 300, RewardPoints = rewardPointRanges[2].Points },
                new Transaction { CreditCardId = creditCards[1].CreditCardId, TransactionTypeId = transactionTypes[0].TransactionTypeId, TransactionDate = new DateTime(2023, 03, 10, 9, 0, 0), Amount = 700, RewardPoints = rewardPointRanges[0].Points },
                new Transaction { CreditCardId = creditCards[2].CreditCardId, TransactionTypeId = transactionTypes[1].TransactionTypeId, TransactionDate = new DateTime(2023, 01, 25, 13, 30, 0), Amount = 1500, RewardPoints = rewardPointRanges[3].Points },
                new Transaction { CreditCardId = creditCards[3].CreditCardId, TransactionTypeId = transactionTypes[2].TransactionTypeId, TransactionDate = new DateTime(2023, 04, 05, 14, 0, 0), Amount = 2000, RewardPoints = rewardPointRanges[5].Points },
                new Transaction { CreditCardId = creditCards[4].CreditCardId, TransactionTypeId = transactionTypes[3].TransactionTypeId, TransactionDate = new DateTime(2023, 05, 15, 16, 0, 0), Amount = 2500, RewardPoints = rewardPointRanges[7].Points },
                new Transaction { CreditCardId = creditCards[5].CreditCardId, TransactionTypeId = transactionTypes[2].TransactionTypeId, TransactionDate = new DateTime(2023, 06, 20, 12, 30, 0), Amount = 800, RewardPoints = rewardPointRanges[4].Points },
                new Transaction { CreditCardId = creditCards[6].CreditCardId, TransactionTypeId = transactionTypes[1].TransactionTypeId, TransactionDate = new DateTime(2023, 07, 25, 11, 0, 0), Amount = 1800, RewardPoints = rewardPointRanges[3].Points }
            };
            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
