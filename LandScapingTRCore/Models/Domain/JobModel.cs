namespace LandscapingTR.Core.Models.Domain
{
    public class JobModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the job name.
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job type id.
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the date of the job.
        /// </summary>
        public DateTime JobDate { get; set; }

        /// <summary>
        /// Gets or sets the location id.
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the first crew member id.
        /// </summary>
        public int? FirstCrewMemberId { get; set; }

        /// <summary>
        /// Gets or sets the second crew member id.
        /// </summary>
        public int? SecondCrewMemberId { get; set; }

        /// <summary>
        /// Gets or sets the third crew member id.
        /// </summary>
        public int? ThirdCrewMemberId { get; set; }

        /// <summary>
        /// Gets or sets the fourth crew member id.
        /// </summary>
        public int? FourthCrewMemberId { get; set; }

        /// <summary>
        /// Gets or sets the crew supervisor id.
        /// </summary>
        public int? CrewSupervisorId { get; set; }

        /// <summary>
        /// Gets or sets the landscape designer id.
        /// </summary>
        public int? LandscapeDesignerId { get; set; }

        /// <summary>
        /// Gets or sets the equipment and safety officer id.
        /// </summary>
        public int? EquipmentAndSafetyOfficerId { get; set; }

        /// <summary>
        /// Gets or sets the estimated Total Hours
        /// </summary>
        public double EstimatedTotalHours { get; set; }

        /// <summary>
        /// Gets or sets the total logged hours.
        /// </summary>
        public double TotalLoggedHours { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the job is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the job is in progress.
        /// </summary>
        public bool InProgress { get; set; }
    }
}
