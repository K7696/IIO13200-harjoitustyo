using System;
using System.ComponentModel.DataAnnotations;

namespace CoreBusinessObjects.Models
{
    public abstract class BaseClass
    {
        #region Properties

        /// <summary>
        /// Owner company
        /// </summary>
        public int CompanyId { get; set; }

        public Guid ObjectId { get; set; }
        public string ShortCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Modified { get; set; }

        public int CreatorId { get; set; }
        public int ModifierId { get; set; }

        #endregion Properties
    }
}
