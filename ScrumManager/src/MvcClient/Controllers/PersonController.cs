using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrumManager.Models;
using CoreBusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcClient.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        #region Fields

        private ApiClient<Person> apiClient;
        private string url;

        #endregion // Fields

        #region Constructors

        public PersonController()
        {
            apiClient = new ApiClient<Person>();
            url = ApiUrl + "persons/1";
        }

        #endregion // Constructors

        #region Public JSON methods

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<JsonResult> GetList()
        {
            List<Person> list = new List<Person>();

            try
            {
                list = await apiClient.GetList(url, BearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(list);
        }

        /// <summary>
        /// Get my details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<JsonResult> GetMyDetails()
        {
            Person obj = new Person();
            try
            {
                var userId = (from c in User.Claims
                              where c.Type == "sub"
                              select new { c.Type, c.Value }).FirstOrDefault();

                int id = int.Parse(userId.Value);

                obj = await apiClient.GetObject(url + "/" + id, BearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(obj);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<JsonResult> Get(int id)
        {
            Person obj = new Person();
            try
            {
                obj = await apiClient.GetObject(url + "/" + id.ToString(), BearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(obj);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Update([FromBody]Person obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.PersonId.ToString(), ApiHttpMethod.PUT, BearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(obj);
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpDelete]
        public async System.Threading.Tasks.Task<JsonResult> Delete(Person obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.PersonId.ToString(), ApiHttpMethod.DELETE, BearerToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(null);
        }

        #endregion // Public JSON methods
    }
}
