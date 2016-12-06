using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;
using CoreBusinessObjects.Models;
using ScrumManager.Models;
using System;
using System.Linq;

namespace MvcClient.Controllers
{
    public class HomeController : BaseController
    {
        private ApiClient<Person> apiClient = new ApiClient<Person>();

        public IActionResult Index()
        {
        
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secure()
        {
            Person obj = new Person();
            string loc = "~/home/error/";
            try
            {
                var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

                var userId = (from c in User.Claims
                              where c.Type == "sub"
                              select new { c.Type, c.Value }).FirstOrDefault();

                int id = int.Parse(userId.Value);

                obj = await apiClient.GetObject(ApiUrl + "persons/1/" + id, accessToken);

                if (obj != null)
                {
                    switch (obj.Role.RoleId)
                    {
                        case 3:
                            loc = "~/index/scrummaster/";
                            break;
                        case 2:
                            loc = "~/index/productowner/";
                            break;
                        case 1:
                        case 4:
                            loc = "~/index/developer/";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Redirect(loc);
        }

        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}