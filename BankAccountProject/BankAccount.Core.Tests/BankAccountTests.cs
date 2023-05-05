using BankAccount.Core.Data;
using BankAccount.Core.Data.DbContext;
using BankAccount.Core.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;


namespace BankAccount.Core.Tests
{
    public class BankAccountTests
    {
        private readonly AccountService _accountService;
        private BankAccountDbContext _context;

        /// <summary>
        /// Instead of Assert. native package of xUnit, I'm using Shouldly Nugget Package for more comprehension
        /// </summary>

        public BankAccountTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            EnsureDatabase.Created(connection);

            var contextOptions = new DbContextOptionsBuilder<BankAccountDbContext>()
                .UseSqlite(connection)
                .Options;

            _context = new BankAccountDbContext(contextOptions);

            _context.Accounts.Add(new Data.Account()
            {
                Id = "6be525dd-a552-4b22-8567-45cc0b88f37f",
                Balance = 1000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            _context.Transactions.Add(new Data.Transaction()
            {
                Id = "1104dcf8-20da-42e3-ae5b-99e7f0cd0c06",
                AccountId = "6be525dd-a552-4b22-8567-45cc0b88f37f",
                Amount = 500,
                Type = TransactionType.Withdraw
            });

            _context.Transactions.Add(new Data.Transaction()
            {
                Id = "6636f322-890c-461a-b3b1-2fc6aae98aa0",
                AccountId = "6be525dd-a552-4b22-8567-45cc0b88f37f",
                Amount = 200,
                Type = TransactionType.Deposit
            });
            _context.SaveChanges();

            _accountService = new AccountService(_context);
        }

        [Fact]
        public void Should_Deposit_An_Amount()
        {
            // Arrange
            var accountId = "6be525dd-a552-4b22-8567-45cc0b88f37f";
            var initialBalance = _accountService.GetBalance(accountId);

            // Act
            var amountToDeposit = 500;
            var result = _accountService.DepositAsync(accountId, amountToDeposit).GetAwaiter().GetResult();
            result.ShouldBe(Data.TransactionState.Success);

            var newBalance = _accountService.GetBalance(accountId);

            // Assert
            newBalance.ShouldBe(initialBalance + amountToDeposit);
        }

        [Fact]
        public void Should_Withdraw_An_Amount()
        {
            // Arrange
            var accountId = "6be525dd-a552-4b22-8567-45cc0b88f37f";
            var initialBalance = _accountService.GetBalance(accountId);

            // Act
            var amountToWithdraw = 500;
            var result = _accountService.WithdrawAsync(accountId, amountToWithdraw).GetAwaiter().GetResult();
            result.ShouldBe(Data.TransactionState.Success);

            var newBalance = _accountService.GetBalance(accountId);

            // Assert
            newBalance.ShouldBe(initialBalance - amountToWithdraw);
        }

        [Fact]
        public void Should_Not_Return_Previous_Balance_After_A_Deposit()
        {
            // Arrange
            var accountId = "6be525dd-a552-4b22-8567-45cc0b88f37f";
            var previousBalance = _accountService.GetBalance(accountId);
            int amount = 540;

            _accountService.DepositAsync(accountId, amount).GetAwaiter().GetResult();

            // Act
            var actualBalance = _accountService.GetBalance(accountId);

            // Assert
            actualBalance.ShouldBeGreaterThan(previousBalance);
        }

        [Fact]
        public void Should_Not_Return_Previous_Balance_After_A_Withdraw()
        {
            // Arrange
            var accountId = "6be525dd-a552-4b22-8567-45cc0b88f37f";
            var previousBalance = _accountService.GetBalance(accountId);
            int amount = 540;

            _accountService.WithdrawAsync(accountId, amount).GetAwaiter().GetResult();

            // Act
            var actualBalance = _accountService.GetBalance(accountId);

            // Assert
            actualBalance.ShouldBeLessThan(previousBalance);
        }

        [Fact]
        public void Should_Return_List_Of_Previous_Transactions()
        {
            // Arrange
            var accountId = "6be525dd-a552-4b22-8567-45cc0b88f37f";
            _accountService.DepositAsync(accountId, 1000).GetAwaiter().GetResult();

            // Act
            var transactions = _accountService.GetPreviousTransactionsAsync(accountId).GetAwaiter().GetResult();

            // Assert
            transactions.Count.ShouldBe(3);
        }
    }
}