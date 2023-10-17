using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService LocationService;

        public LocationsController(ILocationService locationService)
        {
            this.LocationService = locationService;
        }

        [HttpGet]
        [Route("AllLocations")]
        public async Task<IActionResult> GetJobTypes()
        {
            var locationsModels = await this.LocationService.GetLocationsByStateAsync("Arizona");

            if (locationsModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(locationsModels);
        }
    }
}