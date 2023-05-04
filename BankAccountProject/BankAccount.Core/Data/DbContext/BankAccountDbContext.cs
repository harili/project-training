using Microsoft.EntityFrameworkCore;

namespace BankAccount.Core.Data.DbContext
{
    public class BankAccountDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One to Many relation between Account - Transaction
            modelBuilder.Entity<Account>().HasMany(x => x.Transactions).WithOne(x => x.Account).IsRequired();
            modelBuilder.Entity<Transaction>().HasOne(x => x.Account).WithMany(x => x.Transactions);

            //Database seed
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    Id = "1cef3cf8-0b55-4c27-b4b0-304de69b5f19",
                    Balance = 1000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                new Account()
                {
                    Id = "c3321328-4339-4018-8a21-bdeac63135e3",
                    Balance = 500,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction()
                {
                    Id = "63dd9448-f8d7-4b9d-a216-f95b37d962f6",
                    AccountId = "1cef3cf8-0b55-4c27-b4b0-304de69b5f19",
                    Amount = 100,
                    Type = TransactionType.Deposit
                },
                new Transaction()
                {
                    Id = "3da46932-dfde-4e76-b082-0d29c8abf8ef",
                    AccountId = "1cef3cf8-0b55-4c27-b4b0-304de69b5f19",
                    Amount = 80,
                    Type = TransactionType.Withdraw
                },
                new Transaction()
                {
                    Id = "a71f69f0-4415-4776-81fb-07a0f8030548",
                    AccountId = "c3321328-4339-4018-8a21-bdeac63135e3",
                    Amount = 800,
                    Type = TransactionType.Withdraw
                },
                new Transaction()
                {
                    Id = "108dc9bd-3610-44a4-b135-d4b090795f27",
                    AccountId = "c3321328-4339-4018-8a21-bdeac63135e3",
                    Amount = 100,
                    Type = TransactionType.Deposit
                }
            );
        }
    }
}