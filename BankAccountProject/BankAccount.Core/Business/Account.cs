﻿using System.Transactions;

namespace BankAccount.Core.Business
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
