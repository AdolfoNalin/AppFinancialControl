using AppFinancialControl.Models;
using LiteDB;
using System.Collections.ObjectModel;

namespace AppFinancialControl.Service
{
    public class GoalsService : IGoalsService
    {
        private LiteDatabase _db;
        private readonly string _name = "Goals";
        public GoalsService(LiteDatabase database)
        {
            _db = database;
        }

        #region GetUserId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        ObservableCollection<Goals> IGoalsService.GetUserId(Guid userId)
        {
            try
            {
                //if (userId == Guid.Empty)
                //{
                //    throw new NullReferenceException("Não existe usuário");
                //}
                //else
                //{
                    ObservableCollection<Goals> observableGoals = new ObservableCollection<Goals>();   
                    List<Goals> list = _db.GetCollection<Goals>(_name).Query().OrderBy(g => g.Date).ToList();
                    list.ForEach(g =>
                    {
                        observableGoals.Add(g);
                    });

                    return observableGoals;
                //}
            }
            catch(NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// 
        /// </summary>
        /// <param name="goals"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        bool IGoalsService.Insert(Goals goals)
        {
            try
            {
                bool result = false;
                if(goals == null)
                {
                    throw new NullReferenceException("Preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<Goals>(_name).Insert(goals);
                    result = true;
                }

                return result;
            }
            catch(NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update
        bool IGoalsService.Update(Goals goals)
        {
            try
            {
                bool result = false;
                if (goals == null)
                {
                    throw new NullReferenceException("Preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<Goals>().Update(goals);
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

        #region Delete
        bool IGoalsService.Delete(Guid id)
        {
            try
            {
                bool result = false;
                if(id == Guid.Empty)
                {
                    throw new NullReferenceException("Meta não em contrada");
                }
                else
                {
                    _db.GetCollection<Goals>().Delete(id);
                    result = true;
                }

                return result;
            }
            catch(NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
