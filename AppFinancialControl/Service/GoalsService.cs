using AppFinancialControl.Libraries;
using AppFinancialControl.Models;
using LiteDB;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Json;

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
        /// Function responsable search goals referenci parameter userid
        /// </summary>
        /// <param name="userId">Id user</param>
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
            catch (NullReferenceException nre)
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
        /// Function responsable for insert goals in database localhosts
        /// </summary>
        /// <param name="goals"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        String IGoalsService.Insert(Goals goals)
        {
            try
            {
                if (goals == null)
                {
                    throw new NullReferenceException("Preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<Goals>(_name).Insert(goals);
                    return "Meta foi cadastrada com Sucesso";
                }
            }
            catch (NullReferenceException nre)
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
        /// <summary>
        /// Function responsable for Update in localhost
        /// </summary>
        /// <param name="goals"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        String IGoalsService.Update(Goals goals)
        {
            try
            {
                if (goals == null)
                {
                    throw new NullReferenceException("Preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<Goals>().Update(goals);
                    return "Meta foi cadastrada com sucesso";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Function responsable for delete localhost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        String IGoalsService.Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new NullReferenceException("Meta não em contrada");
                }
                else
                {
                    _db.GetCollection<Goals>().Delete(id);
                    return "Meta foi deletada com sucesso";
                }

            }
            catch (NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetUserIdAPI
        /// <summary>
        /// Method responsable for Get the goals in database Postgree
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ObservableCollection<Goals>> GetUserIdAPI(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new NullReferenceException("Usuário não edentificado");
                }
                else
                {
                    ObservableCollection<Goals> ObsControllerGoals = null;
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.GetAsync($"Goals/GetId/{userId}");

                    if (response.IsSuccessStatusCode)
                    {
                        ObsControllerGoals = JsonConvert.DeserializeObject<ObservableCollection<Goals>>(await response.Content.ReadAsStringAsync())
                            ?? throw new ArgumentNullException("Nenhuma meta encontrada");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NullReferenceException(await response.Content.ReadAsStringAsync());
                    }
                    else if (response.StatusCode is HttpStatusCode.BadRequest)
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                    return ObsControllerGoals;
                }
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PostAPI
        /// <summary>
        /// Method resposable for inset Goals in database Postgree
        /// </summary>
        /// <param name="goals"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Boolean> PostAPI(Goals goals)
        {
            try
            {
                if (goals == null)
                {
                    throw new NullReferenceException("Por favor preencha todos os campos");
                }
                else
                {
                    bool result = false;
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.PostAsJsonAsync("Goals/Insert", goals);

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<Boolean>(await response.Content.ReadAsStringAsync());
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NullReferenceException(await response.Content.ReadAsStringAsync());
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }

                    return result;
                }
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

        #region UpdateAPI
        /// <summary>
        /// Method resposable for update Goals in database Postgree
        /// </summary>
        /// <param name="goals"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Boolean> UpdateAPI(Goals goals)
        {
            try
            {
                if (goals == null)
                {
                    throw new NullReferenceException("Por favor preencha todos os campos");
                }
                else
                {
                    bool result = false;
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.PutAsJsonAsync("Goals/Update", goals);

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<Boolean>(await response.Content.ReadAsStringAsync());
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NullReferenceException(await response.Content.ReadAsStringAsync());
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }

                    return result;
                }
            }
            catch (NullReferenceException nre)
            {
                throw nre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteAPI
        /// <summary>
        /// Function responsable for Delete Goals in localhost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Boolean> DeleteAPI(Guid id)
        {
            try
            {
                bool result = false;
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.DeleteAsync($"Goals/Delete/{id}");

                if(response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NullReferenceException(await response.Content.ReadAsStringAsync());
                }
                else if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
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
