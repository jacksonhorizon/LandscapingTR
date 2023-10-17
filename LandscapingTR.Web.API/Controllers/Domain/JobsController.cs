using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

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
        [Route("AllJobs")]
        public async Task<IActionResult> GetJobTypes()
        {
            var lookupModels = await this.JobService.GetJobsByDateRangeAsync(DateTime.MinValue, DateTime.Now);

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }
    }
}