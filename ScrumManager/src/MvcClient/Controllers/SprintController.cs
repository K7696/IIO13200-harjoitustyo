using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrumManager.Models;
using CoreBusinessObjects;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcClient.Controllers
{
    [Authorize]
    public class SprintController : BaseController
    {
        #region Fields

        private ApiClient<Sprint> apiClient;
        private string url;

        #endregion // Fields

        #region Constructors

        public SprintController()
        {
            apiClient = new ApiClient<Sprint>();
            url = ApiUrl + "sprints/1";
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
            List<Sprint> list = new List<Sprint>();

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
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<JsonResult> Get(int id)
        {
            Sprint obj = new Sprint();
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
        /// Add
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> Add([FromBody]Sprint obj)
        {
            try
            {
                // Add other properties
                obj.CompanyId = 1;
                obj.ObjectId = Guid.NewGuid();
                obj.Created = DateTime.Now;
                obj.Modified = DateTime.Now;
                obj.CreatorId = 1;
                obj.ModifierId = 1;

                obj = await apiClient.PostObject(obj, url, ApiHttpMethod.POST, BearerToken);
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
        public async System.Threading.Tasks.Task<JsonResult> Update([FromBody]Sprint obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.SprintId.ToString(), ApiHttpMethod.PUT, BearerToken);
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
        public async System.Threading.Tasks.Task<JsonResult> Delete([FromBody]Sprint obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.SprintId.ToString(), ApiHttpMethod.DELETE, BearerToken);
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
