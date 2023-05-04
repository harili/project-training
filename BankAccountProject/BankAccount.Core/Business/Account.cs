namespace BankAccount.Core.Business
{
    /// <summary>
    /// Business Logic - Account Model
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Transaction>? Transactions { get; set; }
    }
}
