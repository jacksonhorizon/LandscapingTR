using LandscapingTR.Core.Models.Domain;

namespace LandscapingTR.Core.Interfaces
{
    public interface IJobService
    {
        /// <summary>
        /// Gets the jobs by employee id.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns>The jobs.</returns>
        Task<JobModel> GetJobByIdAsync(int jobId);

        /// <summary>
        /// Gets the jobs by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        Task<List<JobModel>> GetJobsByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets the jobs in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        Task<List<JobModel>> GetJobsByDateRangeAsync(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Gets the jobs by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The jobs.</returns>
        Task<List<JobModel>> GetJobsByJobTypeAsync(int jobTypeId);

        /// <summary>
        /// Adds a job.
        /// </summary>
        /// <param name="jobModel">The job.</param>
        /// <returns>The added job.</returns>
        Task<JobModel> SaveJobAsync(JobModel jobModel);

        /// <summary>
        /// Assigns an employee to a job.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="jobId">The job id.</param>
        /// <returns>The assigned job.</returns>
        Task<JobModel> AssignEmployeeToJobAsync(int employeeId, int jobId);
    }
}
