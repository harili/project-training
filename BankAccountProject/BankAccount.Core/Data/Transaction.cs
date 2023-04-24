using System.Transactions;

namespace BankAccount.Core.Data
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Deposit = 1, Withdraw = 2
    }
}
