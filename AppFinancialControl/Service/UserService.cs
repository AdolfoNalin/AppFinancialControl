using AppFinancialControl.Libraries;
using AppFinancialControl.Models;
using LiteDB;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AppFinancialControl.Service
{
    public class UserService : IUserService
    {
        private readonly LiteDatabase _db;
        private readonly string _collectionName = "User";

        public UserService(LiteDatabase database)
        {
            _db = database;
        }

        #region Delete
        /// <summary>
        /// Método responsável por deletar o usuário
        /// </summary>
        /// <exception cref="ArgumentNullException">Quando o usuário é nulo</exception>
        /// <exception cref="Exception">Geral</exception>
        /// <param name="user">Objeto usuário</param>
        public async void Delete(User user)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException("Usuário é nulo");
                }
                else
                {
                    _db.GetCollection<User>(_collectionName).Delete(user.Id);
                    await DeleteAPI(user);
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

        #region Login
        /// <summary>
        /// Função que verifica se o Usuário é valido para login
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Verdadeiro ou falso</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public User Login(UserLogin user)
        {
            try
            {
                List<User> list = _db.GetCollection<User>(_collectionName).Query().OrderBy(u => u.Id).ToList();
                User userLogin = list.Where<User>(u => u.Login.ToUpper().Contains(user.Login.ToUpper())).FirstOrDefault()
                    ?? throw new ArgumentNullException("Usuário não encontrado", "Verifique o login");

                if (!userLogin.Password.Equals(user.Password))
                {
                    throw new ArgumentException("Verifique a senha", "Senha incorreta");
                }
                else
                {
                    return userLogin;
                }
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// Método que insere o usuário no banco de dados
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException("Usuário é nulo, preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<User>(_collectionName).Insert(user);
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

        #region Update
        /// <summary>
        /// Método responsável por atualizar o usuário
        /// </summary>
        /// <param name="user"></param>
        public void Update(User user)
        {
            try
            {
                if (user is null)
                {
                    throw new ArgumentNullException("Usuário está vazio", "Preencha todos os campos");
                }
                else
                {
                    _db.GetCollection<User>(_collectionName).Update(user);
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

        #region DeleteAPI
        private async Task<String> DeleteAPI(User user)
        {
            try
            {
                string message = "";
                if (user != null)
                {
                    HttpClient client = ConnectionLocalhost.ConnectionAPI();
                    HttpResponseMessage response = await client.DeleteAsync($"User/Delete/{user.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        message = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        message = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    throw new ArgumentNullException("Usuário é nullo");
                }

                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Login
        public async Task<User> LoginAPI(User user)
        {
            try
            {
                User userResponse = null;

                HttpClient client = ConnectionLocalhost.ConnectionAPIUser();
                HttpResponseMessage response = await client.PostAsJsonAsync("User/Login", user);

                if (response.IsSuccessStatusCode)
                {
                    userResponse = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                }

                return userResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertAPI
        public async Task<Boolean> InsertAPI(User user)
        {
            try
            {
                bool result = false;
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.PostAsJsonAsync("User/Insert", user);

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Boolean>(await response.Content.ReadAsStringAsync());
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateAPI
        public async Task<Boolean> UpdateAPI(User user)
        {
            try
            {
                bool result = false;
                HttpClient client = ConnectionLocalhost.ConnectionAPI();
                HttpResponseMessage response = await client.PutAsJsonAsync("User/Update", user);

                if(response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Boolean>(await response.Content.ReadAsStringAsync());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
