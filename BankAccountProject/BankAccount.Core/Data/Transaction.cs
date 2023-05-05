using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Transactions;

namespace BankAccount.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Account Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TransactionType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Deposit = 1, Withdraw = 2
    }

    public enum TransactionState
    {
        Success = 1, Failure = 2, Pending = 3
    }
}
