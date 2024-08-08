using System;
using System.ComponentModel.DataAnnotations;

namespace CreditCardRewardPointsCalculator.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Required]
        public int CreditCardId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        public CreditCard CreditCard { get; set; }
        public TransactionType TransactionType { get; set; }

        public int RewardPoints { get; set; }
    }
}
