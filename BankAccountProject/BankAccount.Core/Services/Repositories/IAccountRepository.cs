using BankAccount.Core.Business;

namespace BankAccount.Core.Services.Repositories
{
    /// <summary>
    /// Interface which implements business actions of a bank account
    /// </summary>
    public interface IAccountRepository
    {
        Task<Data.TransactionState> DepositAsync(string accountId, decimal amount);
        Task<Data.TransactionState> WithdrawAsync(string accountId, decimal amount);
        decimal GetBalance(string accountId);
        Task<List<Transaction>> GetPreviousTransactionsAsync(string accountId, int? type = null);
    }
}
