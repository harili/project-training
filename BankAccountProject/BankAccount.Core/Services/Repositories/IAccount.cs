using BankAccount.Core.Business;

namespace BankAccount.Core.Services.Repositories
{
    public interface IAccountService
    {
        void Deposit(Account account, Transaction transaction);
        void Withdraw(Account account, Transaction transaction);
        decimal GetBalance(Account account);
        List<System.Transactions.Transaction> GetPreviousTransactions(int accountId);
    }
}
