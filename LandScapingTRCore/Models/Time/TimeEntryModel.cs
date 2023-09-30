namespace LandscapingTR.Core.Models.Time
{
    public class TimeEntryModel
    {
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the employee type id.
        /// </summary>
        public int EmployeeTypeId { get; set; }

        /// <summary>
        /// Gets or sets job type id.
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the job id.
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the total logged hours.
        /// </summary>
        public double TotalLoggedHours { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets whether the time entry is submitted.
        /// </summary>
        public bool IsSubmitted { get; set; }

        /// <summary>
        /// Gets or sets whether the time entry is approved.
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
