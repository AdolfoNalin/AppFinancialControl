using AppFinancialControl.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
        public List<Transaction> GetAll()
        {
            try
            {
                List<Transaction> list = _db.GetCollection<Transaction>(_collectionName).Query().OrderBy(t => t.Date).ToList();

                if(list is null)
                    throw new ArgumentNullException("Nenhuma transação encontrada");

                return list;
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