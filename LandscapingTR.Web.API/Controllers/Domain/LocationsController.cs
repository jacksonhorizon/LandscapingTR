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


    }
}