using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardRewardPointsCalculator.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal CreditLimit { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
