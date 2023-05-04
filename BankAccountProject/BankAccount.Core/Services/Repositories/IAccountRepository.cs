using BankAccount.Core.Business;

namespace BankAccount.Core.Services.Repositories
{
    /// <summary>
    /// Interface which implements business actions of a bank account
    /// </summary>
    public interface IAccountRepository
    {
        Task<Data.TransactionState> Deposit(string accountId, int type, decimal amount);
        Task<Data.TransactionState> Withdraw(string accountId, int type, decimal amount);
        decimal GetBalance(string accountId);
        Task<List<Transaction>> GetPreviousTransactions(string accountId, int? type = null);
    }
}
