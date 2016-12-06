using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using System.Net.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcClient.Controllers
{
    public class BaseController : Controller
    {
        #region Fields 

        public string BearerToken { get; set; }
        public readonly string ApiUrl = "http://localhost:5001/api/";
        public int UserId { get; set; }
        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseController()
        {
            getToken().Wait();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Get token
        /// </summary>
        /// <returns></returns>
        private async Task<string> getToken()
        {
            var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            BearerToken = tokenResponse.AccessToken;

            return tokenResponse.AccessToken;
        }

        #endregion Public methods
    }
}
