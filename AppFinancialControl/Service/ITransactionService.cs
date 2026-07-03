using AppFinancialControl.Models;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public interface ITransactionService
    {
        public ObservableCollection<Transaction> GetAll(Guid id);
        public ObservableCollection<Transaction> GetDate(Guid userId, MonthItem value);
        public void Add(Transaction transaction);
        public void Update(Transaction transaction);
        public void Delete(Transaction transaction);
    }
}
