using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using AppFinancialControl.Models;

namespace AppFinancialControl.Service
{
    public interface ITransactionService
    {
        public List<Transaction> GetAll();
        public void Add(Transaction transaction);
        public void Update(Transaction transaction);
        public void Delete(Transaction transaction);
    }
}
