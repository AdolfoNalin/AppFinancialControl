using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace AppFinancialControl.Service
{
    public class TransactionService : ITransactionService   
    {
        #region GetAll
        public List<Transaction> GetAll()
        {
            try
            {
                return new List<Transaction>();
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

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}