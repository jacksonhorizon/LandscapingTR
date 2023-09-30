using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.Domain
{
    public class Job: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets the job type id.
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the job type.
        /// </summary>
        public JobType JobType { get; set; }
    }
}
