using AutoMapper;
using LandscapingTR.Core.Entities.Time;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Time;

namespace LandscapingTR.Core.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private ITimeEntryRepository TimeEntryRepository;

        private ITimeEntryHistoryRepository TimeEntryHistoryRepository;

        private IJobRepository JobRepository;

        private readonly IMapper Mapper;
        public TimeEntryService(ITimeEntryRepository timeEntryRepository, ITimeEntryHistoryRepository timeEntryHistoryRepository, IJobRepository jobRepository, IMapper mapper)
        {
            this.TimeEntryRepository = timeEntryRepository;
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
            var entities = await this.TimeEntryRepository.GetTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);
            
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
        public async Task<List<TimeEntryModel>> GetTimeEntriesByDateRangeAsync(DateTime? startDate, DateTime? endDate)
        {
            var entities = await this.TimeEntryRepository.GetTimeEntriesByDateRangeAsync(startDate, endDate);

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
            var entities = await this.TimeEntryRepository.GetTimeEntriesByJobTypeAsync(jobTypeId);

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

        /// <summary>
        /// Saves a time entry.
        /// </summary>
        /// <param name="timeEntryModel">The time entry.</param>
        /// <returns>The saved time entry.</returns>
        public async Task<TimeEntryModel> SaveTimeEntryAsync(TimeEntryModel timeEntryModel)
        {
            var timeEntry = this.Mapper.Map<TimeEntry>(timeEntryModel);
            var timeEntryHistory = this.Mapper.Map<TimeEntryHistory>(timeEntryModel);

            if (timeEntryModel.IsSubmitted)
            {
                var job = await this.JobRepository.GetJobByIdAsync(timeEntry.JobId.Value);
                job.TotalLoggedHours += timeEntry.TotalLoggedHours;

                await this.JobRepository.SaveJobAsync(job);
                var savedTimeEntry = await this.TimeEntryRepository.SaveTimeEntryAsync(timeEntry);

                return Mapper.Map<TimeEntryModel>(savedTimeEntry);
            }
            else
            {
                var savedTimeEntry = await this.TimeEntryHistoryRepository.SaveTimeEntryHistoryAsync(timeEntryHistory);

                return Mapper.Map<TimeEntryModel>(savedTimeEntry);
            }
            
        }


        /// <summary>
        /// Saves a list of time entries.
        /// </summary>
        /// <param name="timeEntryModels">The time entries.</param>
        /// <returns>The saved time entry.</returns>
        public async Task SaveTimeEntryRangeAsync(List<TimeEntryModel> timeEntryModels)
        {
            foreach (var timeEntryModel in timeEntryModels)
            {
                var timeEntry = this.Mapper.Map<TimeEntry>(timeEntryModel);
                var timeEntryHistory = this.Mapper.Map<TimeEntryHistory>(timeEntryModel);

                if (timeEntryModel.IsSubmitted)
                {
                    var job = await this.JobRepository.GetJobByIdAsync(timeEntry.JobId.Value);
                    job.TotalLoggedHours += timeEntry.TotalLoggedHours;

                    await this.JobRepository.SaveJobAsync(job);
                    var savedTimeEntry = await this.TimeEntryRepository.SaveTimeEntryAsync(timeEntry);
                }
                else
                {
                    var savedTimeEntry = await this.TimeEntryHistoryRepository.SaveTimeEntryHistoryAsync(timeEntryHistory);
                }
            }
        }
    }
}
