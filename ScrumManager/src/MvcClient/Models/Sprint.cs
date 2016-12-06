using CoreBusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBusinessObjects
{
    public class Sprint : BaseClass
    {
        #region Properties

        public int SprintId { get; set; }
        public int ProjectId { get; set; }
        public int TeamId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion // Properties
    }
}
