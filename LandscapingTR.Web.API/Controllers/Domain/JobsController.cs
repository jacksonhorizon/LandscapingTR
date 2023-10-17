using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
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


    }
}