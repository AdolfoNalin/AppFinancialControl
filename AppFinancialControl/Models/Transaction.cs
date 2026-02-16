using System;
using System.Collections.Generic;
using System.Text;

namespace AppFinancialControl.Models
{
    public enum TransactionType
    { 
        Income,
        Expenses
    }

    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public DateTimeOffset Date { get; set; }
        public float Value { get; set; }
    }
}
