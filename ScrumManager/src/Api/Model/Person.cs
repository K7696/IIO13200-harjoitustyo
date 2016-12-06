using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBusinessObjects.Models
{
    public class Person : BaseClass
    {
        #region Properties

        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }

        // Role
        public int RoleId { get; set; }
        public Roles Role { get; set; }

        // Team
        public int TeamId { get; set; }
        public Team Team { get; set; }

        #endregion // Properties
    }
}
