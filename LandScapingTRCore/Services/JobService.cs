using AutoMapper;
using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Domain;

namespace LandscapingTR.Core.Services
{
    public class JobService : IJobService
    {
        private IJobRepository JobRepository;

        private readonly IMapper Mapper;
        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            this.JobRepository = jobRepository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets the jobs by id.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns>The job.</returns>
        public async Task<JobModel> GetJobByIdAsync(int jobId)
        {
            var job = await JobRepository.GetJobByIdAsync(jobId);
            return Mapper.Map<JobModel>(job);
        }

        /// <summary>
        /// Gets the jobs by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        public async Task<List<JobModel>> GetJobsByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var jobModels = (await JobRepository.GetJobsByEmployeeIdAsync(employeeId, startDate, endDate)).Select(x => Mapper.Map<JobModel>(x)).ToList();
            return jobModels;
        }

        /// <summary>
        /// Gets the jobs by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The jobs.</returns>
        public async Task<List<JobModel>> GetJobsByLocationIdAsync(int locationId)
        {
            var jobModels = (await JobRepository.GetJobsByLocationIdAsync(locationId)).Select(x => Mapper.Map<JobModel>(x)).ToList();
            return jobModels;
        }

        /// <summary>
        /// Gets the jobs in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The jobs.</returns>
        public async Task<List<JobModel>> GetJobsByDateRangeAsync(DateTime? startDate, DateTime? endDate)
        {
            var jobModels = (await JobRepository.GetJobsByDateRangeAsync(startDate, endDate)).Select(x => Mapper.Map<JobModel>(x)).ToList();
            return jobModels;
        }

        /// <summary>
        /// Gets the jobs by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The jobs.</returns>
        public async Task<List<JobModel>> GetJobsByJobTypeAsync(int jobTypeId)
        {
            var jobModels = (await JobRepository.GetJobsByJobTypeAsync(jobTypeId)).Select(x => Mapper.Map<JobModel>(x)).ToList();
            return jobModels;
        }

        /// <summary>
        /// Adds a job.
        /// </summary>
        /// <param name="jobModel">The job.</param>
        /// <returns>The added job.</returns>
        public async Task<JobModel> SaveJobAsync(JobModel jobModel)
        {
            var job = Mapper.Map<Job>(jobModel);
            job = await JobRepository.SaveJobAsync(job);

            return Mapper.Map<JobModel>(job);
        }

        /// <summary>
        /// Assigns an employee to a job.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="jobId">The job id.</param>
        /// <returns>The assigned job.</returns>
        public async Task<JobModel> AssignEmployeeToJobAsync(int employeeId, int jobId)
        {
            var job = await JobRepository.GetJobByIdAsync(jobId);
            if (!job.FirstCrewMemberId.HasValue){
                job.FirstCrewMemberId = employeeId;
            } 
            else if (!job.SecondCrewMemberId.HasValue)
            {
                job.SecondCrewMemberId = employeeId;
            }
            else if (!job.ThirdCrewMemberId.HasValue)
            {
                job.ThirdCrewMemberId = employeeId;
            }
            else if (!job.FourthCrewMemberId.HasValue)
            {
                job.FourthCrewMemberId = employeeId;

            }

            var savedJob = await JobRepository.SaveJobAsync(job);
            return Mapper.Map<JobModel>(savedJob);
        }
    }
}
