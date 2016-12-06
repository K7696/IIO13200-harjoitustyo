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
    public class StoryController : BaseController
    {
        #region Fields

        private ApiClient<Story> apiClient;
        private string url;

        #endregion // Fields

        #region Constructors

        public StoryController()
        {
            apiClient = new ApiClient<Story>();
            url = ApiUrl + "stories/1";
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
            List<Story> list = new List<Story>();

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
            Story obj = new Story();
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
        public async System.Threading.Tasks.Task<JsonResult> Update([FromBody]Story obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.StoryId.ToString(), ApiHttpMethod.PUT, BearerToken);
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
        public async System.Threading.Tasks.Task<JsonResult> Delete(Story obj)
        {
            try
            {
                obj = await apiClient.PostObject(obj, url + "/" + obj.StoryId.ToString(), ApiHttpMethod.DELETE, BearerToken);
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
