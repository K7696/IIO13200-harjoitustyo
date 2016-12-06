using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBusinessObjects.Models;
using api.Model;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CustomersController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public CustomersController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Customer> getCustomers(int companyId)
        {
            var result = from Customers in context.Customers
                         where Customers.CompanyId == companyId
                         select new Customer
                         {
                             CustomerId = Customers.CustomerId,
                             CompanyId = Customers.CompanyId,
                             ShortCode = Customers.ShortCode,
                             Name = Customers.Name,
                             Description = Customers.Description,
                             Created = Customers.Created,
                             Modified = Customers.Modified
                         }
                         as Customer;

            return result;
        }

        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        private IQueryable<Customer> getCustomer(int companyId, int CustomerId)
        {
            var result = from Customers in context.Customers
                         where Customers.CompanyId == companyId && Customers.CustomerId == CustomerId
                         select new Customer
                         {
                             CustomerId = Customers.CustomerId,
                             CompanyId = Customers.CompanyId,
                             ShortCode = Customers.ShortCode,
                             Name = Customers.Name,
                             Description = Customers.Description,
                             Created = Customers.Created,
                             Modified = Customers.Modified
                         }
                         as Customer;

            return result;
        }

        /// <summary>
        /// Map Customer properties for updating the Customer
        /// </summary>
        /// <param name="Customer"></param>
        /// <param name="value"></param>
        private void mapCustomerUpdate(Customer Customer, Customer value)
        {
            Customer.Name = value.Name;
            Customer.ShortCode = value.ShortCode;
            Customer.Description = value.Description;
            Customer.ModifierId = value.ModifierId;
            Customer.Modified = DateTime.Now;
        }

        #endregion Private methods

        #region Get methods

        // GET: api/Customers/1
        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        public IEnumerable<Customer> Get(int companyId)
        {
            var result = getCustomers(companyId)
                .ToList();

            return result;
        }

        // GET: api/Customers/1/2
        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="CustomerId">Customer Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{customerId}")]
        public IActionResult Get(int companyId, int CustomerId)
        {
            try
            {
                var result = getCustomers(companyId)
                    .Where(x => x.CustomerId == CustomerId)
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

        #endregion Get methods

        #region Data altering

        // POST: api/Customers/1
        /// <summary>
        /// Add a new Customer
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyid, [FromBody]Customer value)
        {
            try
            {
                context.Customers.Add(value);
                context.SaveChanges();

                var result = getCustomer(companyid, value.CustomerId)
                    .FirstOrDefault();

                if (result != null)
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }

        // PUT: api/Customers/1/2
        /// <summary>
        /// Update a Customer
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="customerId">Customer Id</param>
        /// <param name="value">Customer entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{customerId}")]
        public IActionResult PUT(int companyId, int customerId, [FromBody]Customer value)
        {
            try
            {
                // This method is only for updating data
                if (customerId < 1)
                    return BadRequest();

                var result = getCustomer(companyId, customerId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                mapCustomerUpdate(result, value);
                context.Customers.Update(result);
                context.SaveChanges();

                var updatedResult = getCustomer(companyId, customerId)
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

        // DELETE: api/Customers/1/2
        /// <summary>
        /// Delete a Customer
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="customerId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{customerId}")]
        public IActionResult Delete(int companyId, int customerId)
        {
            try
            {
                var result = getCustomer(companyId, customerId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Customers.Remove(result);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        #endregion Data altering
    }
}
