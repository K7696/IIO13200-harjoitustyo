using System.Collections.Generic;

namespace CoreBusinessObjects.Models
{
    public class Story : BaseClass
    {
        #region Properties

        public int StoryId { get; set; }
        public int ProjectId { get; set; }
        public int FeatureId { get; set; }
        public int Priority { get; set; }
        public string AcceptanceCriteria { get; set; }

        public ICollection<Item> Tasks { get; set; }

        #endregion // Properties
    }
}
