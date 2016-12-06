using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using api.Model;
using CoreBusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]/{companyId}")]
    [Authorize]
    public class StoriesController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public StoriesController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get stories
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private IQueryable<Story> getStories(int companyId)
        {
            var result = from stories in context.Stories
                         where stories.CompanyId == companyId
                         select new Story
                         {
                             StoryId = stories.StoryId,
                             CompanyId = stories.CompanyId,
                             ShortCode = stories.ShortCode,
                             Name = stories.Name,
                             Priority = stories.Priority,
                             FeatureId = stories.FeatureId,
                             AcceptanceCriteria = stories.AcceptanceCriteria,
                             ProjectId = stories.ProjectId,
                             Description = stories.Description,
                             Created = stories.Created,
                             Modified = stories.Modified,
                             CreatorId = stories.CreatorId,
                             ModifierId = stories.ModifierId,
                             Tasks = (from items in context.Items where items.StoryId == stories.StoryId
                                      select new Item
                                      {
                                          ItemId = items.ItemId,
                                          CompanyId = items.CompanyId,
                                          StoryId = items.StoryId,
                                          FeatureId = items.FeatureId,
                                          ProjectId = items.ProjectId,
                                          ShortCode = items.ShortCode,
                                          Name = items.Name,
                                          Description = items.Description,
                                          UserAssignedTo = items.UserAssignedTo,
                                          Created = items.Created,
                                          Modified = items.Modified,
                                          CreatorId = items.CreatorId,
                                          ModifierId = items.ModifierId,
                                          ObjectId = items.ObjectId,
                                          WorkLeft = items.WorkLeft
                                      }).ToList()                          
                         } as Story;

            return result;
        }

        /// <summary>
        /// Get story
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="storyId"></param>
        /// <returns></returns>
        private IQueryable<Story> getStory(int companyId, int storyId)
        {
            var result = context.Stories
                .Where(x => x.CompanyId == companyId && x.StoryId == storyId);

            return result;
        }

        /// <summary>
        /// Map story properties for updating the story
        /// </summary>
        /// <param name="story"></param>
        /// <param name="value"></param>
        private void mapStoryUpdate(Story story, Story value)
        {
            story.Name = value.Name;
            story.ShortCode = value.ShortCode;
            story.Description = value.Description;
            story.AcceptanceCriteria = value.AcceptanceCriteria;
            story.ModifierId = value.ModifierId;
            story.Priority = value.Priority;
            story.Modified = DateTime.Now;
        }

        #endregion Private methods

        #region Get methods

        // GET: api/stories/1
        /// <summary>
        /// Get all stories
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        public IEnumerable<Story> Get(int companyId)
        {
            var result = getStories(companyId)
                .ToList();

            return result;
        }

        // GET: api/stories/1/2
        /// <summary>
        /// Get a story
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="storyId">Story Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{storyId}")]
        public IActionResult Get(int companyId, int storyId)
        {
            try
            {
                var result = getStories(companyId)
                    .Where(x => x.StoryId == storyId)
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

        // POST: api/stories/1
        /// <summary>
        /// Add a new story
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyid, [FromBody]Story value)
        {
            try
            {
                context.Stories.Add(value);
                context.SaveChanges();

                var result = getStory(companyid, value.StoryId)
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

        // PUT: api/stories/1/2
        /// <summary>
        /// Update a story
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="storyId">Story Id</param>
        /// <param name="value">Story entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{storyId}")]
        public IActionResult PUT(int companyId, int storyId, [FromBody]Story value)
        {
            try
            {
                // This method is only for updating data
                if (storyId < 1)
                    return BadRequest();

                var result = getStory(companyId, storyId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                mapStoryUpdate(result, value);
                context.Stories.Update(result);
                context.SaveChanges();

                var updatedResult = getStory(companyId, storyId)
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

        // DELETE: api/stories/1/2
        /// <summary>
        /// Delete a story
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="storyId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{storyId}")]
        public IActionResult Delete(int companyId, int storyId)
        {
            try
            {
                var result = getStory(companyId, storyId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Stories.Remove(result);
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
