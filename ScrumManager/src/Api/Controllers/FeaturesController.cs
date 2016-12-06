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
    [Route("api/[controller]")]
    [Authorize]
    public class FeaturesController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public FeaturesController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get all features
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Feature> getFeatures(int companyId)
        {
            var result = from features in context.Features
                         where features.CompanyId == companyId
                         select new Feature
                         {
                             FeatureId = features.FeatureId,
                             CompanyId = features.CompanyId,
                             ShortCode = features.ShortCode,
                             Name = features.Name,
                             Description = features.Description,
                             Created = features.Created,
                             Modified = features.Modified
                         }
                         as Feature;

            return result;
        }

        /// <summary>
        /// Get a feature
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="featureId"></param>
        /// <returns></returns>
        private IQueryable<Feature> getFeature(int companyId, int featureId)
        {
            var result = from features in context.Features
                         where features.CompanyId == companyId && features.FeatureId == featureId
                         select new Feature
                         {
                             FeatureId = features.FeatureId,
                             CompanyId = features.CompanyId,
                             ShortCode = features.ShortCode,
                             Name = features.Name,
                             Description = features.Description,
                             Created = features.Created,
                             Modified = features.Modified
                         }
                         as Feature;

            return result;
        }

        /// <summary>
        /// Map feature properties for updating the feature
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="value"></param>
        private void mapFeatureUpdate(Feature feature, Feature value)
        {
            feature.Name = value.Name;
            feature.ShortCode = value.ShortCode;
            feature.Description = value.Description;
            feature.ModifierId = value.ModifierId;
            feature.Modified = DateTime.Now;
        }

        #endregion Private methods

        #region Get methods

        // GET: api/features/1
        /// <summary>
        /// Get all features
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        public IEnumerable<Feature> Get(int companyId)
        {
            var result = getFeatures(companyId)
                .ToList();

            return result;
        }

        // GET: api/features/1/2
        /// <summary>
        /// Get a feature
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="featureId">feature Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{featureId}")]
        public IActionResult Get(int companyId, int featureId)
        {
            try
            {
                var result = getFeatures(companyId)
                    .Where(x => x.FeatureId == featureId)
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

        // POST: api/features/1
        /// <summary>
        /// Add a new feature
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyid, [FromBody]Feature value)
        {
            try
            {
                context.Features.Add(value);
                context.SaveChanges();

                var result = getFeature(companyid, value.FeatureId)
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

        // PUT: api/features/1/2
        /// <summary>
        /// Update a feature
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="featureId">feature Id</param>
        /// <param name="value">feature entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{featureId}")]
        public IActionResult PUT(int companyId, int featureId, [FromBody]Feature value)
        {
            try
            {
                // This method is only for updating data
                if (featureId < 1)
                    return BadRequest();

                var result = getFeature(companyId, featureId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                mapFeatureUpdate(result, value);
                context.Features.Update(result);
                context.SaveChanges();

                var updatedResult = getFeature(companyId, featureId)
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

        // DELETE: api/features/1/2
        /// <summary>
        /// Delete a feature
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="featureId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{featureId}")]
        public IActionResult Delete(int companyId, int featureId)
        {
            try
            {
                var result = getFeature(companyId, featureId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Features.Remove(result);
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
