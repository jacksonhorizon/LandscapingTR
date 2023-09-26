﻿using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.Domain
{
    public class Job: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the difficulty type id.
        /// </summary>
        public int DifficultyTypeId { get; set; }

        /// <summary>
        /// Gets or sets the difficulty type.
        /// </summary>
        public DifficultyType DifficultyType { get; set; }

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