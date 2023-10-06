using AutoMapper;
using LandscapingTR.Core.Entities.Time;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Time;

namespace LandscapingTR.Core.Services
{
    public class TimeEntryHistoryService : ITimeEntryHistoryService
    {
        private ITimeEntryHistoryRepository TimeEntryHistoryRepository;

        private IJobRepository JobRepository;

        private readonly IMapper Mapper;
        public TimeEntryHistoryService(ITimeEntryHistoryRepository timeEntryHistoryRepository, IJobRepository jobRepository, IMapper mapper)
        {
            this.TimeEntryHistoryRepository = timeEntryHistoryRepository;
            this.JobRepository = jobRepository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets the time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var entities = await this.TimeEntryHistoryRepository.GetTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (entities == null)
            {
                return new List<TimeEntryModel>();
            }
            else
            {
                return entities.Select(x => {
                    var model = this.Mapper.Map<TimeEntryModel>(x);
                    model.JobTypeId = x.Job.JobTypeId;
                    return model;
                }).ToList();
            }
        }

        /// <summary>
        /// Gets the submitted time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetSubmittedTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var entities = await this.TimeEntryHistoryRepository.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (entities == null)
            {
                return new List<TimeEntryModel>();
            }
            else
            {
                return entities.Select(x => this.Mapper.Map<TimeEntryModel>(x)).ToList();
            }
        }

        /// <summary>
        /// Gets the time entries in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetTimeEntriesByDateRangeAsync(DateTime? startDate, DateTime? endDate)
        {
            var entities = await this.TimeEntryHistoryRepository.GetTimeEntriesByDateRangeAsync(startDate, endDate);

            if (entities == null)
            {
                return new List<TimeEntryModel>();
            }
            else
            {
                return entities.Select(x => this.Mapper.Map<TimeEntryModel>(x)).ToList();
            }
        }

        /// <summary>
        /// Gets the time entries by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetTimeEntriesByJobTypeAsync(int jobTypeId)
        {
            var entities = await this.TimeEntryHistoryRepository.GetTimeEntriesByJobTypeAsync(jobTypeId);

            if (entities == null)
            {
                return new List<TimeEntryModel>();
            }
            else
            {
                return entities.Select(x => this.Mapper.Map<TimeEntryModel>(x)).ToList();
            }
        }

        /// <summary>
        /// Gets the time entries by job.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetTimeEntriesByJobIdAsync(int jobId)
        {
            var entities = await this.TimeEntryHistoryRepository.GetTimeEntriesByJobIdAsync(jobId);

            if (entities == null)
            {
                return new List<TimeEntryModel>();
            }
            else
            {
                return entities.Select(x => this.Mapper.Map<TimeEntryModel>(x)).ToList();
            }
        }
    }
}
