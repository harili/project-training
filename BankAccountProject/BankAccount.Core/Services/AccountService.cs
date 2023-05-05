using BankAccount.Core.Business;
using BankAccount.Core.Data.DbContext;
using BankAccount.Core.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountService : IAccountRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly BankAccountDbContext _ctx;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AccountService(BankAccountDbContext context)
        {
            _ctx = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<Data.TransactionState> DepositAsync(string accountId, decimal amount)
        {
            var account = await _ctx.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (account != null)
            {
                if (amount <= 0)
                    return Data.TransactionState.Failure;

                account.Balance += amount;
                account.UpdatedAt = DateTime.Now;

                AddTransaction(accountId, Data.TransactionType.Deposit, amount);

                await _ctx.SaveChangesAsync();

                return Data.TransactionState.Success;
            }

            return Data.TransactionState.Failure;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public decimal GetBalance(string accountId)
        {
            return _ctx.Accounts.Where(x => x.Id == accountId).First().Balance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<Transaction>> GetPreviousTransactionsAsync(string accountId, int? type = null)
        {
            var query = _ctx.Transactions.Where(x => x.AccountId == accountId);

            if (type.HasValue)
            {
                var transactionType = (Data.TransactionType)type.Value;
                query = query.Where(x => x.Type == transactionType);
            }

            return await query.Select(x =>
                new Transaction
                {
                    Amount = x.Amount,
                    AccountId = x.AccountId,
                    Date = x.Date,
                    Id = x.Id,
                    Type = x.Type == Data.TransactionType.Withdraw ? (int)Data.TransactionType.Withdraw : (int)Data.TransactionType.Deposit
                }
            ).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<Data.TransactionState> WithdrawAsync(string accountId, decimal amount)
        {
            var account = await _ctx.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();

            if (account != null)
            {
                if (amount < 0)
                    return Data.TransactionState.Failure;

                if (amount < account.Balance)
                {
                    account.Balance -= amount;
                    account.UpdatedAt = DateTime.Now;

                    AddTransaction(accountId, Data.TransactionType.Withdraw, amount);

                    await _ctx.SaveChangesAsync();
                    return Data.TransactionState.Success;
                }
                else
                    return Data.TransactionState.Failure;
            }

            return Data.TransactionState.Failure;
        }

        private void AddTransaction(string accountId, Data.TransactionType transactionType, decimal amount)
        {
            _ctx.Transactions.Add(new Data.Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,
                AccountId = accountId,
                Date = DateTime.Now,
                Type = transactionType
            });
        }
    }
}
