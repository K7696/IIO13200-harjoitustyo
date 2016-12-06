using api.Model;
using CoreBusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CoreBusinessObjects
{
    namespace MvcMovie.Models
    {
        /// <summary>
        /// Class for initializing database data
        /// </summary>
        public static class SeedData
        {
            #region Private seed methods


            /// <summary>
            /// Init roles
            /// </summary>
            /// <param name="context"></param>
            private static void initRoles(ApiContext context)
            {
                // Look for any Role
                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }

                context.Roles.AddRange(
                   new Roles
                   {
                       ObjectId = Guid.NewGuid(),
                       CompanyId = 1,
                       ShortCode = "",
                       Name = "Developer",
                       Description = "",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   },
                   new Roles
                   {
                       ObjectId = Guid.NewGuid(),
                       CompanyId = 1,
                       ShortCode = "",
                       Name = "Product owner",
                       Description = "",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   },
                   new Roles
                   {
                       ObjectId = Guid.NewGuid(),
                       CompanyId = 1,
                       ShortCode = "",
                       Name = "Scrum master",
                       Description = "",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   },
                   new Roles
                   {
                       ObjectId = Guid.NewGuid(),
                       CompanyId = 1,
                       ShortCode = "",
                       Name = "Tester",
                       Description = "",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   }
                );
            }

            /// <summary>
            /// Init company
            /// </summary>
            /// <param name="context"></param>
            private static void initCompany(ApiContext context)
            {
                // Look for any Companies
                if (context.Items.Any())
                {
                    return;   // DB has been seeded
                }

                context.Companies.AddRange(
                   new Company {
                       ObjectId = Guid.NewGuid(),
                       ShortCode = "CO",
                       Name = "Pasi Coding Company Oy",
                       Description = "Pro",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   }
                );
            }

            /// <summary>
            /// Init customer
            /// </summary>
            /// <param name="context"></param>
            private static void initCustomer(ApiContext context)
            {
                // Look for any Customers
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customers.AddRange(
                    new Customer
                    {
                        CompanyId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "Al Oy",
                        Name = "Ankkalinna Oy",
                        Description = "Ankkalinnan kaupunki",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },   
                    new Customer
                    {
                        CompanyId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "Jkl Oy",
                        Name = "Jyväskylän kaupunki",
                        Description = "Jyväskylän kaupunki",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    }
                );
            }

            /// <summary>
            /// Init team
            /// </summary>
            /// <param name="context"></param>
            private static void initTeam(ApiContext context)
            {
                // Look for any teams
                if (context.Teams.Any())
                {
                    return;   // DB has been seeded
                }

                context.Teams.AddRange(
                   new Team
                   {
                       CompanyId = 1,
                       ObjectId = Guid.NewGuid(),
                       ShortCode = "ST1",
                       Name = "Scrum team 1",
                       Description = "Pro team",
                       Created = DateTime.Now,
                       Modified = DateTime.Now,
                       CreatorId = 1,
                       ModifierId = 1
                   }
                );
            }

            /// <summary>
            /// Init persons
            /// </summary>
            /// <param name="context"></param>
            private static void initPersons(ApiContext context)
            {
                // Look for any persons
                if (context.Persons.Any())
                {
                    return;   // DB has been seeded
                }

                context.Persons.AddRange(
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 3, // Scrum master
                        Firstname = "Aku",
                        Lastname = "Ankka",
                        Email = "aku.ankka@scrum.com",
                        Password = "12345",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "AA",
                        Name = "Aku Ankka",
                        Description = "Scrum master",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 2, // Product owner
                        Firstname = "Iines",
                        Lastname = "Ankka",
                        Email = "iines.ankka@scrum.com",
                        Password = "54321",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "IA",
                        Name = "Iines Ankka",
                        Description = "Product owner",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 1, // Developer
                        Firstname = "Tupu",
                        Lastname = "Ankka",
                        Email = "tupu.ankka@scrum.com",
                        Password = "12345",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "TA",
                        Name = "Tupu Ankka",
                        Description = "Devaaja",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 1, // Developer
                        Firstname = "Hupu",
                        Lastname = "Ankka",
                        Email = "hupu.ankka@scrum.com",
                        Password = "12345",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "HA",
                        Name = "Hupu Ankka",
                        Description = "Devaaja",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 1, // Developer
                        Firstname = "Lupu",
                        Lastname = "Ankka",
                        Email = "lupu.ankka@scrum.com",
                        Password = "12345",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "LA",
                        Name = "Lupu Ankka",
                        Description = "Devaaja",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Person
                    {
                        CompanyId = 1,
                        TeamId = 1,
                        RoleId = 4, // Test
                        Firstname = "Mikki",
                        Lastname = "Hiiri",
                        Email = "mikki.hiiri@scrum.com",
                        Password = "12345",
                        Phonenumber = "0451429100",
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "MH",
                        Name = "Mikki Hiiri",
                        Description = "Testaaja",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    }
                );

            }

            /// <summary>
            /// Init projects
            /// </summary>
            /// <param name="context"></param>
            private static void initProjects(ApiContext context)
            {
                // Look for any projects
                if (context.Projects.Any())
                {
                    return;   // DB has been seeded
                }

                context.Projects.AddRange(
                    new Project
                    {
                        CustomerId = 1,
                        StartDate = DateTime.Now,
                        Deadline = DateTime.Now.AddDays(30),
                        CompanyId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "Proggis 1",
                        Name = "Ohjelmoinnin päättötyö",
                        Description = "Pitää tehdä ohjelmointikurssin päättötyö.",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Project
                    {
                        CustomerId = 1,
                        StartDate = DateTime.Now,
                        Deadline = DateTime.Now.AddDays(30),
                        CompanyId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "Proggis 2",
                        Name = "Kyberturvallisuuden kirjallinen tehtävä",
                        Description = "Pitää tehdä päättötyö.",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    }
                );
            }

            /// <summary>
            /// Init sprints
            /// </summary>
            /// <param name="context"></param>
            private static void initSprints(ApiContext context)
            {
                // Look for any sprints
                if (context.Sprints.Any())
                {
                    return;   // DB has been seeded
                }

                context.Sprints.AddRange(
                    new Sprint
                    {
                        CompanyId = 1,
                        ProjectId = 1,
                        TeamId = 1,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(14), 
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "Sprint1",
                        Name = "Sprint 1",
                        Description = "Sprint 1",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    }    
                );
            }

            private static void initFeatures(ApiContext context)
            {
                // Look for any features
                if (context.Features.Any())
                {
                    return;   // DB has been seeded
                }

                context.Features.AddRange(
                    new Feature
                    {
                        CompanyId = 1,
                        ProjectId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "F1",
                        Name = "Feature 1",
                        Description = "Feature 1",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Feature
                    {
                        CompanyId = 1,
                        ProjectId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "F2",
                        Name = "Feature 2",
                        Description = "Feature 2",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Feature
                    {
                        CompanyId = 1,
                        ProjectId = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "F3",
                        Name = "Feature 3",
                        Description = "Feature 3",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    } 
                );
            }

            /// <summary>
            /// Init stories
            /// </summary>
            /// <param name="context"></param>
            private static void initStories(ApiContext context)
            {
                // Look for any stories
                if (context.Stories.Any())
                {
                    return;   // DB has been seeded
                }

                context.AddRange(
                    new Story {
                        CompanyId = 1,
                        ProjectId = 1,
                        FeatureId = 1,
                        AcceptanceCriteria = "Muista lisätä hyväksyntäkriteerit",
                        Priority = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "S1",
                        Name = "Story 1",
                        Description = "Story 1",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Story {
                        CompanyId = 1,
                        ProjectId = 1,
                        FeatureId = 1,
                        AcceptanceCriteria = "Muista lisätä hyväksyntäkriteerit",
                        Priority = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "S2",
                        Name = "Story 2",
                        Description = "Story 2",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    },
                    new Story {
                        CompanyId = 1,
                        ProjectId = 1,
                        FeatureId = 1,
                        AcceptanceCriteria = "Muista lisätä hyväksyntäkriteerit",
                        Priority = 1,
                        ObjectId = Guid.NewGuid(),
                        ShortCode = "S3",
                        Name = "Story 3",
                        Description = "Story 3",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatorId = 1,
                        ModifierId = 1
                    }
                );
            }

            /// <summary>
            /// Init items
            /// </summary>
            /// <param name="context"></param>
            private static void initItems(ApiContext context)
            {
                // Look for any Items
                if (context.Items.Any())
                {
                    return;   // DB has been seeded
                }

                context.Items.AddRange(
                     new Item
                     {
                         CompanyId = 1,
                         ProjectId = 1,
                         FeatureId = 1,
                         StoryId = 1,
                         UserAssignedTo = 1,
                         WorkLeft = 7.5,
                         ObjectId = Guid.NewGuid(),
                         ShortCode = "T1",
                         Name = "Tietokantaskriptien tekeminen",
                         Description = "Pitää tehdä tietokantaa varten taulujen luontiskriptit.",
                         Created = DateTime.Now,
                         Modified = DateTime.Now,
                         CreatorId = 1,
                         ModifierId = 1
                     },

                     new Item
                     {
                         CompanyId = 1,
                         ProjectId = 1,
                         FeatureId = 1,
                         StoryId = 1,
                         UserAssignedTo = 1,
                         WorkLeft = 7.5,
                         ObjectId = Guid.NewGuid(),
                         ShortCode = "T1",
                         Name = "Identityserverin konfigurointi",
                         Description = "Identityserver pitää konfiguroida tunnistamaan käyttäjät.",
                         Created = DateTime.Now,
                         Modified = DateTime.Now,
                         CreatorId = 1,
                         ModifierId = 1
                     },

                     new Item
                     {
                         CompanyId = 1,
                         ProjectId = 1,
                         FeatureId = 1,
                         StoryId = 1,
                         UserAssignedTo = 1,
                         WorkLeft = 7.5,
                         ObjectId = Guid.NewGuid(),
                         ShortCode = "T1",
                         Name = "WebApin kontrollien tekeminen",
                         Description = "WebApin endpointit pitää koodata valmiiksi.",
                         Created = DateTime.Now,
                         Modified = DateTime.Now,
                         CreatorId = 1,
                         ModifierId = 1
                     }
                );
            }

            #endregion // Private seed methods

            #region Initializer

            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new ApiContext(
                    serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
                {
                    // Init roles
                    initRoles(context);

                    // Init company
                    initCompany(context);

                    // Init customer
                    initCustomer(context);

                    // Init projects
                    initProjects(context);

                    // Init sprint
                    initSprints(context);

                    // Init features
                    initFeatures(context);

                    // Init stories
                    initStories(context);

                    // Init items
                    initItems(context);

                    // Init team
                    initTeam(context);

                    // Finally save data
                    context.SaveChanges();

                    // Init persons
                    initPersons(context);

                    context.SaveChanges();
                }
            }

            #endregion // Initializer
        }
    }
}
