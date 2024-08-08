using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardRewardPointsCalculator.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }
}
