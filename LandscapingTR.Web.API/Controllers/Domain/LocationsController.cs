using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Domain;

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
        [Route("GetAllLocations")]
        public async Task<IActionResult> GetLocationTypes()
        {
            var locationsModels = await this.LocationService.GetLocationsByStateAsync("Arizona");

            if (locationsModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(locationsModels);
        }

        [HttpGet]
        [Route("Location")]
        public async Task<IActionResult> GetLocationById(int locationId)
        {
            var locationModel = await this.LocationService.GetLocationByIdAsync(locationId);

            if (locationModel == null)
            {
                return BadRequest();
            }

            return Ok(locationModel);
        }

        [HttpGet]
        [Route("LocationByCity")]
        public async Task<IActionResult> GetlocationsByCity(string city)
        {
            var locationModels = await this.LocationService.GetLocationsByCityAsync(city);

            if (locationModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(locationModels);
        }

        [HttpGet]
        [Route("LocationByState")]
        public async Task<IActionResult> GetlocationsByState(string state)
        {
            var locationModels = await this.LocationService.GetLocationsByCityAsync(state);

            if (locationModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(locationModels);
        }

        [HttpGet]
        [Route("LocationByLocationType")]
        public async Task<IActionResult> GetlLcationsByLocationTypeId(int locationTypeId)
        {
            var locationModels = await this.LocationService.GetLocationsByLocationTypeAsync(locationTypeId);

            if (locationModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(locationModels);
        }

        [HttpPost]
        [Route("location")]
        public async Task<IActionResult> SaveNewlocation(LocationModel locationModel)
        {
            var savedlocationModel = await this.LocationService.SaveLocationAsync(locationModel);

            if (savedlocationModel == null)
            {
                return BadRequest();
            }

            return Ok(savedlocationModel);
        }

        [HttpPut]
        [Route("location")]
        public async Task<IActionResult> Savelocation(LocationModel locationModel)
        {
            var savedlocationModel = await this.LocationService.SaveLocationAsync(locationModel);

            if (savedlocationModel == null)
            {
                return BadRequest();
            }

            return Ok(savedlocationModel);
        }
    }
}