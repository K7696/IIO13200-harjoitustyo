﻿using CoreBusinessObjects.Models;
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
        public ICollection<Person> Persons { get; set; }

        #endregion // Properties
    }
}
