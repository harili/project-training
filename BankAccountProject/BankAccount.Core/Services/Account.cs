using BankAccount.Core.Business;
using BankAccount.Core.Services.Repositories;

namespace BankAccount.Core.Services
{
    public class AccountService
    {
        private readonly IAccountService _accountService;
        public AccountService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Data.Transaction SaveTransaction(Account account, Transaction transaction)
        {
            if (transaction is null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction.Amount < 0)
                throw new ArgumentException("AMOUNT_NOT_VALID");

            switch (transaction.Type)
            {
                case 1: // Dépôt
                    _accountService.Deposit(account, transaction); 
                    break;

                case 2: // Retrait
                    _accountService.Withdraw(account, transaction);
                    break;

                default:
                    throw new ArgumentException($"Invalid transaction type: {transaction.Type}");
            }

            return CreateTransactionObject<Data.Transaction>(transaction);
        }

        private TTransaction CreateTransactionObject<TTransaction>(Transaction transaction) where TTransaction
            : Data.Transaction, new()
        {
            return new TTransaction
            {
                AccountId = transaction.AccountId,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Id = transaction.Id,
                Type = (Data.TransactionType)transaction.Type
            };
        }
    }
}
