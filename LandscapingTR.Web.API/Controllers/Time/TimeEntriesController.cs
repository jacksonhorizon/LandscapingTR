using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
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


    }
}