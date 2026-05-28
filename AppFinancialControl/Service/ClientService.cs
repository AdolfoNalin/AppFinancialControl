using AppFinancialControl.Libraries;
using AppFinancialControl.Models;
using LiteDB;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppFinancialControl.Service
{
    internal class ClientService : IClientService
    {
        private static LiteDatabase _db;
        private static readonly string _colectionName = "Client";

        public ClientService(LiteDatabase database)
        {
            _db = database;
        }

        #region Add
        /// <summary>
        /// Function responsable for Insert client in database localhost
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<string> Add(Client client)
        {
            try
            {
                if (client == null)
                {
                    throw new ArgumentNullException("Cliente é null");
                }
                else
                {
                    ClientSession.Id = client.Id;
                    _db.GetCollection<Client>(_colectionName).Insert(client);
                    if (await InsertAPI(client))
                    {
                        return "Certo, Vamos para a segunda etapa";
                    }
                    else
                    {
                        return "Algo deu errado";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(Client client)
        {
            try
            {
                bool result = false;
                if (client == null)
                {
                    throw new ArgumentNullException("Cliente é nulo");
                }
                else
                {
                    _db.GetCollection<Client>(_colectionName).Delete(client.Id);
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAll
        public ObservableCollection<Client> GetAll()
        {
            try
            {
                List<Client> list = _db.GetCollection<Client>(_colectionName).Query().OrderBy(c => c.Name).ToList()
                    ?? throw new ArgumentNullException("Lista de Cliente está vazia");

                ObservableCollection<Client> obsControllers = new ObservableCollection<Client>();

                list.ForEach(c => obsControllers.Add(c));

                return obsControllers;
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        public Boolean Update(Client client)
        {
            try
            {
                bool result = false;
                if (client == null)
                {
                    throw new ArgumentNullException("Cliente é nulo");
                }
                else
                {
                    _db.GetCollection<Client>(_colectionName).Update(client);
                    result = true;
                }

                return result;
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertAPI
        /// <summary>
        /// Method responsable for Insert in API Postgre
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<Boolean> InsertAPI(Client client)
        {
            try
            {
                bool result = false;
                HttpClient httpClient = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("Client/Insert", client);

                result = JsonConvert.DeserializeObject<Boolean>(await response.Content.ReadAsStringAsync());

                return result;
            }
            catch (LiteException le)
            {
                throw le;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteAPI
        public async Task<String> DeleteAPI(Client client)
        {
            try
            {
                if (Delete(client))
                {
                    HttpClient htppClient = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await htppClient.DeleteAsync($"Client/Delete/{client.Id}");

                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                else
                {
                    throw new LiteException(404, "Cleinte não foi deletado");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAllAPI
        public async Task<ObservableCollection<Client>> GetAllAPI()
        {
            try
            {
                ObservableCollection<Client> getLocalhost = GetAll();
                if (getLocalhost.Count > 0)
                {
                    ObservableCollection<Client> getAPI = new ObservableCollection<Client>();
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.GetAsync($"{_colectionName}/GetAll");

                    if (response.IsSuccessStatusCode)
                    {
                        getAPI = JsonConvert.DeserializeObject<ObservableCollection<Client>>(await response.Content.ReadAsStringAsync())
                            ?? throw new ArgumentNullException();

                        if (getAPI.Count != getLocalhost.Count)
                        {
                            throw new ArgumentException("Sem conexão com a internet");
                        }
                        else
                        {
                            getAPI.Clear();
                            return getLocalhost;
                        }
                    }
                    else
                    {
                        string message = await response.Content.ReadAsStringAsync();
                        throw new Exception(message);
                    }
                }
                else
                {
                    throw new Exception("Algo deu errado");
                }
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateAPI
        public async Task<String> UpdateAPI(Client client)
        {
            try
            {
                if (Update(client))
                {
                    HttpClient httpClient = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{_colectionName}/Update", client);

                    string message = await response.Content.ReadAsStringAsync();

                    return message;
                }
                else
                {
                    throw new Exception("Não foi possivél realizar a atualização");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
