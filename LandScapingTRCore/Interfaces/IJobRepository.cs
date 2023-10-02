using LandscapingTR.Core.Entities.Domain;

namespace LandscapingTR.Core.Interfaces
{
    public interface IJobRepository : IRepository<Job, int?>
    {
        /// <summary>
        /// Gets a job by job id.
        /// </summary>
        /// <param name="jobId">The employee id.</param>
        /// <returns>The jobs.</returns>
        Task<Job> GetJobIdAsync(int jobId);


        /// <summary>
        /// Gets the jobs by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        Task<List<Job>> GetJobsByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets the jobs in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        Task<List<Job>> GetJobsByDateRangeAsync(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Gets the jobs by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The jobs.</returns>
        Task<List<Job>> GetJobsByJobTypeAsync(int jobTypeId);

        /// <summary>
        /// Saves a job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns>The saved job.</returns>
        Task<Job> SaveJobAsync(Job job);

        /// <summary>
        /// Saves a list of jobs.
        /// </summary>
        /// <param name="jobs">The jobs.</param>
        /// <returns>The saved jobs.</returns>
        Task<List<Job>> SaveJobRangeAsync(List<Job> jobs);
    }
}
