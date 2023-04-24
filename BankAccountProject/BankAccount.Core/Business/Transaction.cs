using System.Runtime.ConstrainedExecution;
using System.Text;
using System;
using System.Transactions;

namespace BankAccount.Core.Business
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
