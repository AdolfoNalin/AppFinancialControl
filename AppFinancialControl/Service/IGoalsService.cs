using AppFinancialControl.Models;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public interface IGoalsService
    {
        public ObservableCollection<Goals> GetUserId(Guid userId);
        public String Insert(Goals goals);
        public String Update(Goals goals);
        public String Delete(Guid id);
    }
}
