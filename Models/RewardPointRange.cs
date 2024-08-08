using System.ComponentModel.DataAnnotations;

namespace CreditCardRewardPointsCalculator.Models
{
    public class RewardPointRange
    {
        public int RewardPointRangeId { get; set; }

        [Required]
        public int TransactionTypeId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MinAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MaxAmount { get; set; }

        [Required]
        public int Points { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
