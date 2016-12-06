using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBusinessObjects.Models;
using api.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]/{companyId}")]
    public class PersonsController : Controller
    {
        #region Fields

        private ApiContext context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ctx"></param>
        public PersonsController(ApiContext ctx)
        {
            context = ctx;
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Get a person
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="personId">Person Id</param>
        /// <returns>IQueryable</returns>
        private IQueryable<Person> getPerson(int companyId, int personId)
        {
            var result = from persons in context.Persons
                         join role in context.Roles on persons.RoleId equals role.RoleId 
                         join team in context.Teams on persons.TeamId equals team.TeamId
                         where persons.CompanyId == companyId && persons.PersonId == personId
                         select new Person {
                             PersonId = persons.PersonId,
                             CompanyId = persons.CompanyId,
                             ShortCode = persons.ShortCode,
                             Created = persons.Created,
                             Modified = persons.Modified,
                             Name = persons.Name,
                             ObjectId = persons.ObjectId,
                             Firstname = persons.Firstname,
                             Lastname = persons.Lastname,
                             Email = persons.Email,
                             Phonenumber = persons.Phonenumber,
                             Description = persons.Description,
                             Role = new CoreBusinessObjects.Roles
                             {
                                 RoleId = role.RoleId,
                                 Name = role.Name,
                                 Created = role.Created,
                                 Modified = role.Modified
                             },
                             Team = new CoreBusinessObjects.Team
                             {
                                 TeamId = team.TeamId,
                                 ShortCode = team.ShortCode,
                                 Name = team.Name,
                                 Description = team.Description,
                                 Created = team.Created,
                                 Modified = team.Modified
                             }
                         }
                         
                         ;

            return result;
        }

        /// <summary>
        /// Get persons
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns>IQueryable</returns>
        private IQueryable<Person> getPersons(int companyId)
        {
            var result = from persons in context.Persons
                         join role in context.Roles on persons.RoleId equals role.RoleId
                         join team in context.Teams on persons.TeamId equals team.TeamId
                         where persons.CompanyId == companyId 
                         select new Person
                         {
                             PersonId = persons.PersonId,
                             CompanyId = persons.CompanyId,
                             ShortCode = persons.ShortCode,
                             Created = persons.Created,
                             Modified = persons.Modified,
                             Name = persons.Name,
                             ObjectId = persons.ObjectId,
                             Firstname = persons.Firstname,
                             Lastname = persons.Lastname,
                             Email = persons.Email,
                             Phonenumber = persons.Phonenumber,
                             Description = persons.Description,
                             Role = new CoreBusinessObjects.Roles
                             {
                                 RoleId = role.RoleId,
                                 Name = role.Name,
                                 Created = role.Created,
                                 Modified = role.Modified
                             },
                             Team = new CoreBusinessObjects.Team
                             {
                                 TeamId = team.TeamId,
                                 ShortCode = team.ShortCode,
                                 Name = team.Name,
                                 Description = team.Description,
                                 Created = team.Created,
                                 Modified = team.Modified
                             }
                         }

                         ;

            return result;
        }

        /// <summary>
        /// Map person properties for updating the person
        /// </summary>
        /// <param name="person">Person to update</param>
        /// <param name="value">Person with new values</param>
        private void mapPersonUpdate(Person person, Person value)
        {
            // Map properties
            person.Firstname = value.Firstname;
            person.Lastname = value.Lastname;
            person.Description = value.Description;
            person.ShortCode = value.ShortCode;
            person.Email = value.Email;
            // person.Password = value.Password;
            person.Name = value.Name;
            person.Phonenumber = value.Phonenumber;
            person.Modified = DateTime.Now;
            person.ModifierId = value.ModifierId;
        }

        #endregion Private methods


        #region Get methods

        // GET: api/persons/1
        /// <summary>
        /// Get all persons
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <returns></returns>
        public IEnumerable<Person> Get(int companyId)
        {
            var result = getPersons(companyId)
                .ToList();

            return result;
        }

        // GET: api/persons/1/2
        /// <summary>
        /// Get a person
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="personId">Person Id</param>
        /// <returns></returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpGet("{personId}")]
        public IActionResult Get(int companyId, int personId)
        {
            try
            {
                var result = getPerson(companyId, personId)
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

        // POST: api/persons/1
        /// <summary>
        /// Add a new person
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="value"></param>
        /// <returns>Created entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        [HttpPost]
        public IActionResult Post(int companyid, [FromBody]Person value)
        {
            try
            {
                context.Persons.Add(value);
                context.SaveChanges();

                var result = getPerson(companyid, value.PersonId)
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

        // PUT: api/persons/1/2
        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="companyId">Company Id</param>
        /// <param name="personId">Person Id</param>
        /// <param name="value">Person entity</param>
        /// <returns>Updated entity</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpPut("{personId}")]
        public IActionResult PUT(int companyId, int personId, [FromBody]Person value)
        {
            try
            {
                // This method is only for updating data
                if (personId < 1)
                    return BadRequest();

                var result = context.Persons.Where(x => x.CompanyId == companyId && x.PersonId == personId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                // Map person properties
                mapPersonUpdate(result, value);
                context.Persons.Update(result);
                context.SaveChanges();

                var updatedResult = getPerson(companyId, personId)
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

        // DELETE: api/persons/1/2
        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="personId"></param>
        /// <returns>StatusCode</returns>
        /// <statusCode="200">Ok</statusCode>
        /// <statusCode="400">Bad request</statusCode>
        /// <statusCode="404">Not found</statusCode>
        [HttpDelete("{personId}")]
        public IActionResult Delete(int companyId, int personId)
        {
            try
            {
                var result = context.Persons.Where(x => x.CompanyId == companyId && x.PersonId == personId)
                    .FirstOrDefault();

                if (result == null)
                    return NotFound();

                context.Persons.Remove(result);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        #endregion // Data altering
    }
}
