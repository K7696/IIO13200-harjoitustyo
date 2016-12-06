using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Model;
using CoreBusinessObjects;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]/{companyId}")]
    [Authorize]
    public class SprintsController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public SprintsController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get all sprints
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Sprint> getSprints(int companyId)
        {
            var result = from sprints in context.Sprints
                         where sprints.CompanyId == companyId
                          select new Sprint
                          {
                              SprintId = sprints.SprintId,
                              CompanyId = sprints.CompanyId,
                              ProjectId = sprints.ProjectId,
                              ObjectId = sprints.ObjectId,
                              CreatorId = sprints.CreatorId,
                              ModifierId = sprints.ModifierId,
                              ShortCode = sprints.ShortCode,
                              Name = sprints.Name,
                              StartDate = sprints.StartDate,
                              EndDate = sprints.EndDate,
                              Description = sprints.Description,
                              TeamId = sprints.TeamId,
                              Created = sprints.Created,
                              Modified = sprints.Modified
                          }
                          as Sprint;

            return result;
        }

        /// <summary>
        /// Get a sprint
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        private IQueryable<Sprint> getSprint(int companyId, int sprintId)
        {
            var result = from sprints in context.Sprints
                         where sprints.CompanyId == companyId && sprints.SprintId == sprintId
                         select new Sprint
                         {
                             SprintId = sprints.SprintId,
                             CompanyId = sprints.CompanyId,
                             ProjectId = sprints.ProjectId,
                             ObjectId = sprints.ObjectId,
                             CreatorId = sprints.CreatorId,
                             ModifierId = sprints.ModifierId,
                             ShortCode = sprints.ShortCode,
                             Name = sprints.Name,
                             StartDate = sprints.StartDate,
                             EndDate = sprints.EndDate,
                             Description = sprints.Description,
                             TeamId = sprints.TeamId,
                             Created = sprints.Created,
                             Modified = sprints.Modified
                         }
                         as Sprint;

            return result;
        }

        /// <summary>
        /// Map sprint properties for updating the sprint
        /// </summary>
        /// <param name="sprint"></param>
        /// <param name="value"></param>
        private void mapSprintUpdate(Sprint sprint, Sprint value)
        {
            sprint.Name = value.Name;
            sprint.ShortCode = value.ShortCode;
            sprint.Description = value.Description;
            sprint.StartDate = value.StartDate;
            sprint.EndDate = value.EndDate;
            sprint.ModifierId = value.ModifierId;
            sprint.Modified = DateTime.Now;
        }

        #endregion Private methods

        #region Get methods

        // GET: api/sprints/1
        /// <summary>
        /// Get all sprints
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        public IEnumerable<Sprint> Get(int companyId)
        {
            var result = getSprints(companyId)
                .ToList();

            return result;
        }

        // GET: api/sprints/1/2
        /// <summary>
        /// Get a sprint
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="sprintId">Sprint Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{sprintId}")]
        public IActionResult Get(int companyId, int sprintId)
        {
            try
            {
                var result = getSprints(companyId)
                    .Where(x => x.SprintId == sprintId)
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

        // POST: api/sprints/1
        /// <summary>
        /// Add a new sprint
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyid, [FromBody]Sprint value)
        {
            try
            {
                context.Sprints.Add(value);
                context.SaveChanges();

                var result = getSprint(companyid, value.SprintId)
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

        // PUT: api/sprints/1/2
        /// <summary>
        /// Update a sprint
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="sprintId">Sprint Id</param>
        /// <param name="value">Sprint entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{sprintId}")]
        public IActionResult PUT(int companyId, int sprintId, [FromBody]Sprint value)
        {
            try
            {
                // This method is only for updating data
                if (sprintId < 1)
                    return BadRequest();

                var result = getSprint(companyId, sprintId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                mapSprintUpdate(result, value);
                context.Sprints.Update(result);
                context.SaveChanges();

                var updatedResult = getSprint(companyId, sprintId)
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

        // DELETE: api/sprint/1/2
        /// <summary>
        /// Delete a sprint
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="sprintId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{sprintId}")]
        public IActionResult Delete(int companyId, int sprintId)
        {
            try
            {
                var result = getSprint(companyId, sprintId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Sprints.Remove(result);
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
