using LandscapingTR.Core.Entities.Time;

namespace LandscapingTR.Core.Interfaces
{
    public interface ITimeEntryRepository : IRepository<TimeEntry, int?>
    {
        /// <summary>
        /// Gets the time entries by the id.
        /// </summary>
        /// <param name="timeEntryId">The time entry id.</param>
        /// <returns>The time entries.</returns>
        Task<TimeEntry> GetTimeEntryByIdAsync(int timeEntryId);

        /// <summary>
        /// Gets the time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        Task<List<TimeEntry>> GetTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets the submitted time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        Task<List<TimeEntry>> GetSubmittedTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets the time entries in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        Task<List<TimeEntry>> GetTimeEntriesByDateRangeAsync(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Gets the time entries by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The time entries.</returns>
        Task<List<TimeEntry>> GetTimeEntriesByJobTypeAsync(int jobTypeId);

        /// <summary>
        /// Gets the time entries by job.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns>The time entries.</returns>
        Task<List<TimeEntry>> GetTimeEntriesByJobIdAsync(int jobId);

        /// <summary>
        /// Saves a time entry.
        /// </summary>
        /// <param name="timeEntry">The time entry.</param>
        /// <returns>The saved time entry.</returns>
        Task<TimeEntry> SaveTimeEntryAsync(TimeEntry timeEntry);


        /// <summary>
        /// Saves a list of time entries.
        /// </summary>
        /// <param name="timeEntries">The time entries.</param>
        /// <returns>The saved time entry.</returns>
        Task<List<TimeEntry>> SaveTimeEntryRangeAsync(List<TimeEntry> timeEntries);
    }
}
