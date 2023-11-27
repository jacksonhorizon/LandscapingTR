using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Time;

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
            var endDate = DateTime.MaxValue;
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllTimeEntriesByEmployeeIdWithinDates")]
        public async Task<IActionResult> GetAllTimeEntriesByEmployeeId(int employeeId, DateTime? startDate, DateTime? endDate)
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

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllSubmittedTimeEntriesByEmployeeIdWithinDates")]
        public async Task<IActionResult> GetSubmittedTimeEntriesByEmployeeIdWithinDates(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            var timeEntryModels = await this.TimeEntryService.GetSubmittedTimeEntriesByEmployeeIdAsync(employeeId, startDate, endDate);

            return Ok(timeEntryModels);
        }

        [HttpGet]
        [Route("AllSubmittedTimeEntriesWithinDates")]
        public async Task<IActionResult> GetAllSubmittedTimeEntriesWithinDates()
        {
            var timeEntryModels = await this.TimeEntryService.GetTimeEntriesByDateRangeAsync(null, null);

            return Ok(timeEntryModels);
        }

        [HttpPost]
        [Route("SaveTimeEntry")]
        public async Task<IActionResult> SaveTimeEntry(TimeEntryModel timeEntryModel)
        {
            var savedTimeEntryModel = await this.TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            if (savedTimeEntryModel == null)
            {
                return BadRequest();
            }

            return Ok(savedTimeEntryModel);
        }

        [HttpPut]
        [Route("SaveTimeEntry")]
        public async Task<IActionResult> UpdateTimeEntry(TimeEntryModel timeEntryModel)
        {
            var savedTimeEntryModel = await this.TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            if (savedTimeEntryModel == null)
            {
                return BadRequest();
            }

            return Ok(savedTimeEntryModel);
        }

        [HttpPost]
        [Route("SaveTimeEntries")]
        public async Task<IActionResult> SaveTimeEntries(List<TimeEntryModel> timeEntryModels)
        {
            var savedTimeEntryModels = await this.TimeEntryService.SaveTimeEntryRangeAsync(timeEntryModels);

            if (savedTimeEntryModels == null)
            {
                return BadRequest();
            }

            return Ok(savedTimeEntryModels);
        }

        [HttpPut]
        [Route("SaveTimeEntries")]
        public async Task<IActionResult> UpdateTimeEntries(List<TimeEntryModel> timeEntryModels)
        {
            var savedTimeEntryModels = await this.TimeEntryService.SaveTimeEntryRangeAsync(timeEntryModels);

            if (savedTimeEntryModels == null)
            {
                return BadRequest();
            }

            return Ok(savedTimeEntryModels);
        }

        [HttpDelete]
        [Route("DeleteTimeEntry")]
        public async Task<IActionResult> DeleteTimeEntry(TimeEntryModel timeEntryModel)
        {
            var deletedTimeEntryModel = await this.TimeEntryService.DeleteTimeEntry(timeEntryModel);

            return Ok(deletedTimeEntryModel); 
        }
    }
}