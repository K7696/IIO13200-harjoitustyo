using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using CoreBusinessObjects;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CompaniesController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public CompaniesController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get all companies
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Company> getCompanies()
        {
            var result = from companys in context.Companies
                         select new Company
                         {
                             CompanyId = companys.CompanyId,
                             ShortCode = companys.ShortCode,
                             Name = companys.Name,
                             Description = companys.Description,
                             Created = companys.Created,
                             Modified = companys.Modified
                         }
                         as Company;

            return result;
        }

        /// <summary>
        /// Get a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Company> getCompany(int companyId)
        {
            var result = from companys in context.Companies
                         where companys.CompanyId == companyId 
                         select new Company
                         {
                             CompanyId = companys.CompanyId,
                             ShortCode = companys.ShortCode,
                             Name = companys.Name,
                             Description = companys.Description,
                             Created = companys.Created,
                             Modified = companys.Modified
                         }
                         as Company;

            return result;
        }

        /// <summary>
        /// Map company properties for updating the company
        /// </summary>
        /// <param name="company"></param>
        /// <param name="value"></param>
        private void mapCompanyUpdate(Company company, Company value)
        {
            company.Name = value.Name;
            company.ShortCode = value.ShortCode;
            company.Description = value.Description;
            company.ModifierId = value.ModifierId;
            company.Modified = DateTime.Now;
        }

        #endregion Private methods

        #region Get methods

        // GET: api/companies/
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Company> Get()
        {
            var result = getCompanies()
                .ToList();

            return result;
        }

        // GET: api/companies/1
        /// <summary>
        /// Get a company
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{companyId}")]
        public IActionResult Get(int companyId)
        {
            try
            {
                var result = getCompanies()
                    .Where(x => x.CompanyId == companyId)
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

        // POST: api/companies
        /// <summary>
        /// Add a new company
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post([FromBody]Company value)
        {
            try
            {
                context.Companies.Add(value);
                context.SaveChanges();

                var result = getCompany(value.CompanyId)
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

        // PUT: api/companies/1
        /// <summary>
        /// Update a company
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="value">company entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{companyId}")]
        public IActionResult PUT(int companyId, [FromBody]Company value)
        {
            try
            {
                // This method is only for updating data
                if (companyId < 1)
                    return BadRequest();

                var result = getCompany(companyId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                mapCompanyUpdate(result, value);

                context.SaveChanges();

                var updatedResult = getCompany(companyId)
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

        // DELETE: api/companies/1
        /// <summary>
        /// Delete a company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{companyId}")]
        public IActionResult Delete(int companyId)
        {
            try
            {
                var result = getCompany(companyId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Companies.Remove(result);
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
