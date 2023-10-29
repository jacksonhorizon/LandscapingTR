using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class JobRepository : BaseRepository<Job, int?>, IJobRepository
    {
        public JobRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets a job by job id.
        /// </summary>
        /// <param name="jobId">The employee id.</param>
        /// <returns>The jobs.</returns>
        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            return await this.DataContext.Jobs.FirstOrDefaultAsync(x => x.Id == jobId);
        }

        /// <summary>
        /// Gets the time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<Job>> GetJobsByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await this.DataContext.Jobs
                .Where(x => x.FirstCrewMemberId == employeeId ||
                    x.SecondCrewMemberId == employeeId ||
                    x.ThirdCrewMemberId == employeeId ||
                    x.FourthCrewMemberId == employeeId ||
                    x.CrewSupervisorId == employeeId ||
                    x.LandscapeDesignerId == employeeId ||
                    x.EquipmentAndSafetyOfficerId == employeeId &&
                    (startDate == null || endDate == null) ? true : (x.JobDate > startDate && x.JobDate < endDate))
                .ToListAsync();
        }

        /// <summary>
        /// Gets the jobs by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The jobs.</returns>
        public async Task<List<Job>> GetJobsByLocationIdAsync(int locationId)
        {
            return await this.DataContext.Jobs
                .Where(x => x.LocationId == locationId)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the time entries in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<Job>> GetJobsByDateRangeAsync(DateTime? startDate, DateTime? endDate)
        {
            return await this.DataContext.Jobs
                .Where(x => x.JobDate < endDate && x.JobDate > startDate)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the time entries by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<Job>> GetJobsByJobTypeAsync(int jobTypeId)
        {
            return await this.DataContext.Jobs
                .Where(x => x.JobTypeId == jobTypeId)
                .ToListAsync();
        }

        /// <summary>
        /// Saves a time entry.
        /// </summary>
        /// <param name="timeEntry">The time entry.</param>
        /// <returns>The saved time entry.</returns>
        public async Task<Job> SaveJobAsync(Job job)
        {
            if (DataContext.Jobs.FirstOrDefault(x => x.Id == job.Id) != null)
            {
                // Existing employee - update it in the context
                DataContext.Jobs.Update(job);
            }
            else
            {
                // New employee - add it to the context
                job.CreatedDate = DateTime.Now;
                DataContext.Jobs.Add(job);
            }

            await DataContext.SaveChangesAsync();

            return job;
        }

        /// <summary>
        /// Saves a list of time entries.
        /// </summary>
        /// <param name="timeEntries">The time entries.</param>
        /// <returns>The saved time entry.</returns>
        public async Task<List<Job>> SaveJobRangeAsync(List<Job> jobs)
        {
            await this.SaveRangeAsync(jobs);

            return jobs;
        }
    }
}
