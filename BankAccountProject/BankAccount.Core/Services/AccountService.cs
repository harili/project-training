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
            _ctx = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<Core.Data.TransactionState> Deposit(string accountId, int type, decimal amount)
        {
            var account = await _ctx.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();
            if (account != null)
            {
                if (amount <= 0)
                    return Core.Data.TransactionState.Failure;

                account.Balance += amount;
                account.UpdatedAt = DateTime.Now;

                AddTransaction(accountId, type, amount);

                await _ctx.SaveChangesAsync();

                return Core.Data.TransactionState.Success;
            }

            return Core.Data.TransactionState.Failure;
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
        public async Task<List<Transaction>> GetPreviousTransactions(string accountId, int? type = null)
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
        public async Task<Core.Data.TransactionState> Withdraw(string accountId, int type, decimal amount)
        {
            var account = await _ctx.Accounts.Where(x => x.Id == accountId).FirstOrDefaultAsync();

            if (account != null)
            {
                if (amount < 0)
                    return Core.Data.TransactionState.Failure;

                if (amount < account.Balance)
                {
                    account.Balance -= amount;
                    account.UpdatedAt = DateTime.Now;

                    AddTransaction(accountId, type, amount);

                    await _ctx.SaveChangesAsync();
                    return Data.TransactionState.Success;
                }
                else
                    return Data.TransactionState.Failure;
            }

            return Core.Data.TransactionState.Failure;
        }

        private void AddTransaction(string accountId, int type, decimal amount)
        {
            _ctx.Transactions.Add(new Core.Data.Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = amount,
                AccountId = accountId,
                Date = DateTime.Now,
                Type = type == (int)Data.TransactionType.Withdraw ? Data.TransactionType.Withdraw : Data.TransactionType.Deposit
            });
        }
    }
}
