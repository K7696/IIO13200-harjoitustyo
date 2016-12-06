using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBusinessObjects.Models
{
    public class Project : BaseClass
    {
        #region Properties

        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<Sprint> Sprints { get; set; }

        #endregion Properties
    }
}
