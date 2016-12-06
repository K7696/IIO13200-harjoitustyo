using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScrumManager.Models
{
    public enum ApiHttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class ApiClient<T>
    {
        #region Fields

        private string cntType;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiClient()
        {
            cntType = "application/json";
        }

        #endregion // Constructors

        #region Private methods

        /// <summary>
        /// Get list of object properties
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /*private List<KeyValuePair<string, string>> getObjectProperties(T item)
        {
            var pairs = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                pairs.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(item).ToString()));
            }

            return pairs;
        }*/

        #endregion // Private methods

        #region Public methods

        /// <summary>
        /// Get list of items from a web api
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList(string url, string accessToken)
        {
            List<T> items = new List<T>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Set accessToken
                client.SetBearerToken(accessToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
                }
            }

            return items;
        }

        /// <summary>
        /// Get object from a web api
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<T> GetObject(string url, string accessToken)
        {
            T item = default(T);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Set accessToken
                client.SetBearerToken(accessToken);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    item = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
                }
            }

            return item;
        }

        /// <summary>
        /// Post object to web api
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="url"></param>
        /// <param name="apiHttpMethod"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<T> PostObject(T pItem, string url, ApiHttpMethod apiHttpMethod, string accessToken)
        {
            T item = default(T);

            using (var client = new HttpClient())
            {
                string body = string.Empty;

                if(apiHttpMethod == ApiHttpMethod.POST ||
                    apiHttpMethod == ApiHttpMethod.PUT)
                {
                    body = Newtonsoft.Json.JsonConvert.SerializeObject(pItem);
                }

                // Set accessToken
                client.SetBearerToken(accessToken);

                HttpContent content = new StringContent(body);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(cntType);

                HttpResponseMessage response;

                switch (apiHttpMethod)
                {
                    case ApiHttpMethod.POST:
                            response = client.PostAsync(url, content).Result;
                        break;
                    case ApiHttpMethod.PUT:
                            response = client.PutAsync(url, content).Result;
                        break;
                    case ApiHttpMethod.DELETE:
                            response = client.DeleteAsync(url).Result;
                        break;
                    default:
                        throw new NotSupportedException();
                }
           
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    item = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
                }
            }

            return item;
        }

        #endregion // Public methods
    }
}
