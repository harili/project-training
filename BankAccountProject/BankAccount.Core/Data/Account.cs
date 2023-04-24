using System.Transactions;

namespace BankAccount.Core.Data
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
