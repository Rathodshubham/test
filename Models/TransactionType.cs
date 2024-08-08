using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditCardRewardPointsCalculator.Models
{
    public class TransactionType
    {
        public int TransactionTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        public ICollection<RewardPointRange> RewardPointRanges { get; set; } = new List<RewardPointRange>();
    }
}
