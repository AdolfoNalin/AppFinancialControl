using AppFinancialControl.Models;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public interface IClientService
    {
        ObservableCollection<Client> GetAll();
        Task<String> Add(Client client);
        Boolean Update(Client client);
        Boolean Delete(Client client);
    }
}
