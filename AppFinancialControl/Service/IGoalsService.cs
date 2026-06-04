using AppFinancialControl.Models;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public interface IGoalsService
    {
        public ObservableCollection<Goals> GetUserId(Guid userId);
        public Boolean Insert(Goals goals);
        public Boolean Update(Goals goals);
        public Boolean Delete(Guid id);
    }
}
