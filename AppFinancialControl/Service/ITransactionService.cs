using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Transactions;

namespace AppFinancialControl.Service
{
    interface ITransactionService
    {
        public List<Transaction> GetAll();
        public void Add(Transaction transaction);
        public void Update(Transaction transaction);
        public void Delete(Transaction transaction);
    }
}
