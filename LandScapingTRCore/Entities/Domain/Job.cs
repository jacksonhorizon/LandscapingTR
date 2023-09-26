using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.Domain
{
    public class Job: BaseEntity
    {
        /// <summary>
        /// The employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The employee.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// The difficulty type id.
        /// </summary>
        public int DifficultyTypeId { get; set; }

        /// <summary>
        /// The difficulty type.
        /// </summary>
        public DifficultyType DifficultyType { get; set; }

        /// <summary>
        /// The job type id.
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// The job type.
        /// </summary>
        public JobType JobType { get; set; }
    }
}
