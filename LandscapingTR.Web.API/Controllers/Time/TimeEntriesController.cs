using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Time
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService TimeEntryService;

        public TimeEntriesController(ITimeEntryService timeEntryService)
        {
            this.TimeEntryService = timeEntryService;
        }

        [HttpGet]
        [Route("AllTimeEntries")]
        public async Task<IActionResult> GetJobTypes()
        {
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByDateRangeAsync(DateTime.MinValue, DateTime.Now);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }
    }
}