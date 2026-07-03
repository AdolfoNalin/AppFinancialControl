using AppFinancialControl.Libraries;
using AppFinancialControl.Models;
using LiteDB;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace AppFinancialControl.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly LiteDatabase _db;
        private readonly string _collectionName = "Transactions";
        public TransactionService(LiteDatabase databae)
        {
            _db = databae;
        }

        #region GetAll
        public ObservableCollection<Transaction> GetAll(Guid id)
        {
            try
            {
                List<Transaction> list = _db.GetCollection<Transaction>(_collectionName).Query().Where(u => u.UserId == id).OrderBy(t => t.Date).ToList();

                ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();
                list.ToList().ForEach(i => transactions.Add(i));

                if (list is null)
                    throw new ArgumentNullException("Nenhuma transação encontrada");

                return transactions;
            }
            catch (ArgumentNullException ane)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region GetDate
        public ObservableCollection<Transaction> GetDate(Guid userId, MonthItem value)
        {
            try
            {
                List<Transaction> listTransation = _db.GetCollection<Transaction>(_collectionName).Query().Where(t => t.UserId == userId)
                    .Where(i => i.Date.Month == value.Month && i.Date.Year == value.Year).ToList()
                    ?? throw new ArgumentNullException("Nenhuma transação encontrada");

                ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

                listTransation.ToList().ForEach(i => transactions.Add(i));

                return transactions;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Add
        public void Add(Transaction transaction)
        {
            try
            {
                if (transaction == null)
                    throw new ArgumentNullException("Transação não concluida. Os campos estão vazios");
                else
                {
                    _db.GetCollection<Transaction>(_collectionName).Insert(transaction);
                    InsertAPI(transaction);
                }
            }
            catch (ArgumentNullException ane)
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Update
        public void Update(Transaction transaction)
        {
            try
            {
                if (transaction == null)
                    throw new ArgumentNullException("Transação não concluida. Os campos estão vazios");
                else
                    _db.GetCollection<Transaction>(_collectionName)
                        .Update(transaction);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Delete
        public void Delete(Transaction transaction)
        {
            try
            {
                if (transaction == null)
                    throw new ArgumentNullException("Transação não concluida. Os campos estão vazios");
                else
                    _db.GetCollection<Transaction>(_collectionName)
                        .Delete(transaction.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region GetAll
        public static async Task<ObservableCollection<Transaction>> GetAllAPI(Guid userId)
        {
            try
            {
                ObservableCollection<Transaction> trancations = null;
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.GetAsync($"Transaction/GetAll/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    trancations = JsonConvert.DeserializeObject<ObservableCollection<Transaction>>(await response.Content.ReadAsStringAsync())
                        ?? throw new ArgumentNullException("Nenhuma transação encotrada!");
                }

                return trancations;
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

        #region GetDate
        public static async Task<ObservableCollection<Transaction>> GetDateAPI(Guid userId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                ObservableCollection<Transaction> trancations = null;
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.GetAsync($"Transaction/GetDate/{userId}?startDate={startDate}&endDate{endDate}");

                if (response.IsSuccessStatusCode)
                {
                    trancations = JsonConvert.DeserializeObject<ObservableCollection<Transaction>>(await response.Content.ReadAsStringAsync());
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new NullReferenceException(await response.Content.ReadAsStringAsync());
                }

                return trancations;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertAPI
        public static async Task<String> InsertAPI(Transaction transaction)
        {
            try
            {
                string message = "";
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.PostAsJsonAsync("Transaction/Insert", transaction);

                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    message = await response.Content.ReadAsStringAsync();
                }

                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateAPI
        public async Task<String> UpdateAPI(Transaction transaction)
        {
            try
            {
                string message = "";
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.PutAsJsonAsync("Transaction/Update", transaction);

                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    message = await response.Content.ReadAsStringAsync();
                }

                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        public async Task<String> DeleteAPI(Guid transactionId)
        {
            try
            {
                string message = "";
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.DeleteAsync($"Transaction/Delete/{transactionId}");

                if (response.IsSuccessStatusCode)
                {
                    message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    message = await response.Content.ReadAsStringAsync();
                }

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