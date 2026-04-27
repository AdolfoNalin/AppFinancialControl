using AppFinancialControl.Models;
using LiteDB;
using System.Collections.ObjectModel;

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
        public ObservableCollection<Transaction> GetAll()
        {
            try
            {
                List<Transaction> list = _db.GetCollection<Transaction>(_collectionName).Query().OrderBy(t => t.Date).ToList();

                ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();
                list.ToList().ForEach(i => transactions.Add(i));

                if(list is null)
                    throw new ArgumentNullException("Nenhuma transação encontrada");

                return transactions;
            }
            catch(ArgumentNullException ane)
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
        public ObservableCollection<Transaction> GetDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<Transaction> listTransation = _db.GetCollection<Transaction>(_collectionName).Query()
                    .Where(i => i.Date.Date == startDate.Date.Date && i.Date.Date == endDate.Date.Date).ToList()
                    ?? throw new ArgumentNullException("Nenhuma transação encontrada") ;

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
                    _db.GetCollection<Transaction>(_collectionName)
                        .Insert(transaction);
            }
            catch(ArgumentNullException ane)
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
    }
}