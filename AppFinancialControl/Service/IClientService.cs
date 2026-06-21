using AppFinancialControl.Models;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public interface IClientService
    {
        ObservableCollection<Client> GetAll();
        string Add(Client client);
        string Update(Client client);
        string Delete(Client client);
    }
}
