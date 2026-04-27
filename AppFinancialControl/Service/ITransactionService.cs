using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using AppFinancialControl.Models;

namespace AppFinancialControl.Service
{
    public interface ITransactionService
    {
        public ObservableCollection<Transaction> GetAll();
        public ObservableCollection<Transaction> GetDate(DateTime startDate, DateTime endDate);
        public void Add(Transaction transaction);
        public void Update(Transaction transaction);
        public void Delete(Transaction transaction);
    }
}
