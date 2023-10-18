using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Domain;

namespace LandscapingTR.Web.API.Controllers.Domain
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService JobService;

        public JobsController(IJobService jobService)
        {
            this.JobService = jobService;
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<IActionResult> GetJobTypes()
        {
            var jobModels = await this.JobService.GetJobsByDateRangeAsync(DateTime.MinValue, DateTime.Now);

            if (jobModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(jobModels);
        }

        [HttpGet]
        [Route("Job")]
        public async Task<IActionResult> GetJobById(int jobId)
        {
            var jobModel = await this.JobService.GetJobByIdAsync(jobId);

            if (jobModel == null)
            {
                return BadRequest();
            }

            return Ok(jobModel);
        }

        [HttpGet]
        [Route("JobByEmployeeId")]
        public async Task<IActionResult> GetJobsByEmployeeId(int employeeId)
        {
            var jobModels = await this.JobService.GetJobsByEmployeeIdAsync(employeeId);

            if (jobModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(jobModels);
        }

        [HttpGet]
        [Route("JobByLocationId")]
        public async Task<IActionResult> GetJobsByLocationId(int locationId)
        {
            var jobModels = await this.JobService.GetJobsByLocationIdAsync(locationId);

            if (jobModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(jobModels);
        }

        [HttpGet]
        [Route("JobByDateRange")]
        public async Task<IActionResult> GetJobsByDateRange(DateTime startDate, DateTime endDate)
        {
            var jobModels = await this.JobService.GetJobsByDateRangeAsync(startDate, endDate);

            if (jobModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(jobModels);
        }

        [HttpGet]
        [Route("JobByJobType")]
        public async Task<IActionResult> GetJobsByJobType(int jobTypeId)
        {
            var jobModels = await this.JobService.GetJobsByJobTypeAsync(jobTypeId);

            if (jobModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(jobModels);
        }

        [HttpPost]
        [Route("Job")]
        public async Task<IActionResult> SaveNewJob(JobModel jobModel)
        {
            var savedJobModel = await this.JobService.SaveJobAsync(jobModel);

            if (savedJobModel == null)
            {
                return BadRequest();
            }

            return Ok(savedJobModel);
        }

        [HttpPut]
        [Route("Job")]
        public async Task<IActionResult> SaveJob(JobModel jobModel)
        {
            var savedJobModel = await this.JobService.SaveJobAsync(jobModel);

            if (savedJobModel == null)
            {
                return BadRequest();
            }

            return Ok(savedJobModel);
        }

        [HttpPost]
        [Route("JobAssignEmployee")]
        public async Task<IActionResult> AssignEmployeeToJob(int employeeId, int jobId)
        {
            var savedJobModel = await this.JobService.AssignEmployeeToJobAsync(employeeId, jobId);

            if (savedJobModel == null)
            {
                return BadRequest();
            }

            return Ok(savedJobModel);
        }
    }
}