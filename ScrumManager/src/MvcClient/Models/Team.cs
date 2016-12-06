using CoreBusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBusinessObjects
{
    public class Team : BaseClass
    {
        #region Properties

        public int TeamId { get; set; }
        public List<Person> Persons { get; set; }

        #endregion // Properties

        public Team()
        {
            Persons = new List<Person>();
        }
    }
}
