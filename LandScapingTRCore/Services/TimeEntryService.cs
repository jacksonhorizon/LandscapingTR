using AutoMapper;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Time;

namespace LandscapingTR.Core.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private ITimeEntryRepository TimeEntryRepository;

        private readonly IMapper Mapper;
        public TimeEntryService(ITimeEntryRepository timeEntryRepository, IMapper mapper)
        {
            this.TimeEntryRepository = timeEntryRepository;
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
            var entities = await this.TimeEntryRepository.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);
            
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
        /// Gets the submitted time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntryModel>> GetSubmittedTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var entities = await this.TimeEntryRepository.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

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
        public async Task<List<TimeEntryModel>> GetTimeEntriesByDateRange(DateTime? startDate, DateTime? endDate)
        {
            var entities = await this.TimeEntryRepository.GetTimeEntriesByDateRange(startDate, endDate);

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
        public async Task<List<TimeEntryModel>> GetTimeEntriesByJobType(int jobTypeId)
        {
            var entities = await this.TimeEntryRepository.GetTimeEntriesByJobType(jobTypeId);

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
            var entities = await this.TimeEntryRepository.GetTimeEntriesByJobIdAsync(jobId);

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
