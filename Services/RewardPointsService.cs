using System;
using System.Linq;
using CreditCardRewardPointsCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardRewardPointsCalculator.Services
{
    public class RewardPointsService
    {
        private readonly RewardPointsDbContext _context;

        public RewardPointsService(RewardPointsDbContext context)
        {
            _context = context;
        }

        public void CalculateRewardPoints()
        {
            try
            {
                var transactions = _context.Transactions
                    .Include(t => t.TransactionType)
                    .Include(t => t.CreditCard)
                    .ThenInclude(cc => cc.Customer)
                    .ToList();

                foreach (var transaction in transactions)
                {
                    var rewardPointRange = _context.RewardPointRanges
                        .FirstOrDefault(r => r.TransactionTypeId == transaction.TransactionTypeId &&
                                             transaction.Amount >= r.MinAmount &&
                                             transaction.Amount < r.MaxAmount);

                    if (rewardPointRange != null)
                    {
                        transaction.RewardPoints = rewardPointRange.Points;
                    }
                    else
                    {
                        transaction.RewardPoints = 0; // or some default value
                    }
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating reward points: {ex.Message}");
            }
        }
    }
}
