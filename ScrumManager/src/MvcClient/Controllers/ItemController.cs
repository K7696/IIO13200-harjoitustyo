using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreBusinessObjects.Models;
using System;
using ScrumManager.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcClient.Controllers
{
    [Authorize]
    public class ItemController : BaseController
    {
        #region Fields

        private ApiClient<Item> apiClient;
        private string url;

        #endregion // Fields

        #region Constructors

        public ItemController()
        {
            apiClient = new ApiClient<Item>();
            url = ApiUrl + "items/1";
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
            List<Item> list = new List<Item>();

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
            Item obj = new Item();
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
        public async System.Threading.Tasks.Task<JsonResult> Add([FromBody]Item obj)
        {
            try
            {
                // Add other properties
                obj.FeatureId = 1;
                obj.ProjectId = 1;
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
        public async System.Threading.Tasks.Task<JsonResult> Update([FromBody]Item obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.ItemId.ToString(), ApiHttpMethod.PUT, BearerToken);
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
        public async System.Threading.Tasks.Task<JsonResult> Delete(Item obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.ItemId.ToString(), ApiHttpMethod.DELETE, BearerToken);
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
