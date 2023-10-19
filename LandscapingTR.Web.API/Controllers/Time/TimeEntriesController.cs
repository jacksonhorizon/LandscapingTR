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
        [Route("AllTimeEntriesByEmployeeId")]
        public async Task<IActionResult> GetAllTimeEntriesByEmployeeId(int employeeId)
        {
            var startDate = DateTime.MinValue;
            var endDate = DateTime.Now;
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllTimeEntriesByEmployeeIdWithinDates")]
        public async Task<IActionResult> GetAllTimeEntriesByEmployeeId(int employeeId, DateTime startDate, DateTime endDate)
        {
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllSubmittedTimeEntriesByEmployeeId")]
        public async Task<IActionResult> GetSubmittedTimeEntriesByEmployeeId(int employeeId)
        {
            var startDate = DateTime.MinValue;
            var endDate = DateTime.Now;
            var timeEntryModels = await this.TimeEntryService.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllSubmittedTimeEntriesByEmployeeIdWithinDates")]
        public async Task<IActionResult> GetSubmittedTimeEntriesByEmployeeIdWithinDates(int employeeId, DateTime startDate, DateTime endDate)
        {
            var timeEntryModels = await this.TimeEntryService.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllSubmittedTimeEntriesWithinDates")]
        public async Task<IActionResult> GetAllSubmittedTimeEntriesWithinDates(DateTime startDate, DateTime endDate)
        {
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByDateRangeAsync(startDate, endDate);

            if (timeEntryModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(timeEntryModels);
        }
    }
}