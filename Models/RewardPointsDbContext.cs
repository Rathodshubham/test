using Microsoft.EntityFrameworkCore;

namespace CreditCardRewardPointsCalculator.Models
{
    public class RewardPointsDbContext : DbContext
    {
        public RewardPointsDbContext(DbContextOptions<RewardPointsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<RewardPointRange> RewardPointRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>()
                .Property(c => c.CreditLimit)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RewardPointRange>()
                .Property(r => r.MaxAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RewardPointRange>()
                .Property(r => r.MinAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
