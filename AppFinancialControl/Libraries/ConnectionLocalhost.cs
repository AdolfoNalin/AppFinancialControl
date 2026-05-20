
using AppFinancialControl.Models;
using System.Net;

namespace AppFinancialControl.Libraries
{
    public class ConnectionLocalhost
    {
        #region GetEndpoint
        /// <summary>
        /// Method responsable for Get endpoint the service API
        /// </summary>
        /// <returns></returns>
        private static string GetEndPoint()
        {
            try
            {
                string result = MauiProgram.endPointAPI
                    ?? throw new ArgumentNullException("Endpoint não encontrado!");
                return result;
            }
            catch(ArgumentNullException ane)
            {
                throw ane;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ConnectionAPI
        /// <summary>
        /// Method responsable for Connect the API
        /// </summary>
        /// <returns></returns>
        public static HttpClient ConnectionAPI()
        {
            try
            {
                
                string endpoint = GetEndPoint();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserSession.Token);
                client.Timeout = new TimeSpan(0, 0, 30);

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ConnectionAPIUser
        /// <summary>
        /// Method responsable for connect user in service API
        /// </summary>
        /// <returns></returns>
        public static HttpClient ConnectionAPIUser()
        {
            try
            {
                string endpoint = GetEndPoint();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 0, 30);

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
