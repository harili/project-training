using System.ComponentModel.DataAnnotations;

namespace BankAccount.Core.Data
{
    /// <summary>
    /// Minimal Account Class properties for the challenge
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Transaction>? Transactions { get; set; }
    }
}
