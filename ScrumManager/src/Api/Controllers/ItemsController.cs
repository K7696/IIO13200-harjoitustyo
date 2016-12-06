using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Model;
using CoreBusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]/{companyId}")]
    [Authorize]
    public class ItemsController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiCtx"></param>
        public ItemsController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion // Constructor

        #region Private methods

        /// <summary>
        /// Get single item
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="itemId">Item Id</param>
        /// <returns></returns>
        private IQueryable<Item> getSingleItem(int companyId, int itemId)
        {
            var result = context.Items
                .Where(x => x.CompanyId == companyId && x.ItemId == itemId);

            return result;
        }

        /// <summary>
        /// Map item properties for updating the item
        /// </summary>
        /// <param name="item">Item to update</param>
        /// <param name="value">Item with new values</param>
        private void mapItemUpdate(Item item, Item value)
        {
            item.Name = value.Name;
            item.Description = value.Description;
            item.Modified = DateTime.Now;
            item.UserAssignedTo = value.UserAssignedTo;
            item.ModifierId = value.ModifierId;
            item.ShortCode = value.ShortCode;
            item.WorkLeft = value.WorkLeft;
        }

        #endregion // Private methods

        #region Get methods

        // GET: api/items/1
        /// <summary>
        /// Get all items
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Item> Get(int companyId)
        {
            var result = context.Items
                .Where(x => x.CompanyId == companyId)
                .ToList();

            return result;
        }

        // GET: api/items/5/1
        /// <summary>
        /// Get single item
        /// </summary>
        /// <param name="itemId">Item id</param>
        /// <returns>The item entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{itemId}")]
        public IActionResult Get(int companyId, int itemId)
        {
            try
            {
                var result = getSingleItem(companyId, itemId)
                    .FirstOrDefault();

                if (result != null)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return NotFound();
        }


        #endregion // Get methods

        #region Data altering

        // POST: api/items/5
        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="item">Item entity</param>
        /// <returns>Item entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyId, [FromBody]Item value)
        {
            try
            {
                context.Items.Add(value);
                context.SaveChanges();

                int id = value.ItemId;

                var result = getSingleItem(companyId, id)
                    .FirstOrDefault();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }           
        }

        // PUT: api/items/1/2
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="itemId">Item Id</param>
        /// <param name="value">Item entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{itemId}")]
        public IActionResult PUT(int companyId, int itemId, [FromBody]Item value)
        {
            try
            {
                // This method is only for updating data
                if (itemId < 1)
                    return BadRequest();

                var result = getSingleItem(companyId, itemId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                // Map person properties
                mapItemUpdate(result, value);
                context.Items.Update(result);
                context.SaveChanges();

                var updatedResult = getSingleItem(companyId, itemId)
                    .FirstOrDefault();

                if (updatedResult != null)
                    return Ok(updatedResult);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }


        // DELETE: api/items/1/3
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="itemId">Item Id</param>
        /// <returns>Statuscode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{itemId}")]
        public IActionResult Delete(int companyId, int itemId)
        {
            try
            {
                var result = getSingleItem(companyId, itemId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Items.Remove(result);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        #endregion // Data altering        
    }
}
