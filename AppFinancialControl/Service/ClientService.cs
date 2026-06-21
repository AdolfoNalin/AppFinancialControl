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
        public string Add(Client client)
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

                    return $"Cliente foi cadastrado com sucesso";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public string Delete(Client client)
        {
            try
            {
                if (client == null)
                {
                    throw new ArgumentNullException("Cliente é nulo");
                }
                else
                {
                    _db.GetCollection<Client>(_colectionName).Delete(client.Id);
                    return $"Cliente foi deletado com sucesso";
                }
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
        public string Update(Client client)
        {
            try
            {
                if (client == null)
                {
                    throw new ArgumentNullException("Cliente é nulo");
                }
                else
                {
                    _db.GetCollection<Client>(_colectionName).Update(client);
                    return "Cliente foi atualizado com sucesso";
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
                HttpClient htppClient = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await htppClient.DeleteAsync($"Client/Delete/{client.Id}");

                string message = await response.Content.ReadAsStringAsync();
                return message;

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
                if (true)
                {
                    ObservableCollection<Client> getAPI = new ObservableCollection<Client>();
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.GetAsync($"{_colectionName}/GetAll");

                    if (response.IsSuccessStatusCode)
                    {
                        getAPI = JsonConvert.DeserializeObject<ObservableCollection<Client>>(await response.Content.ReadAsStringAsync())
                            ?? throw new ArgumentNullException();
                        
                        if (true)
                        {
                            throw new ArgumentException("Sem conexão com a internet");
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
                HttpClient httpClient = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{_colectionName}/Update", client);

                string message = await response.Content.ReadAsStringAsync();

                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
