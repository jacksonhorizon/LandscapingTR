using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILookupService LookupService;

        public LocationsController(ILookupService lookupService)
        {
            this.LookupService = lookupService;
        }

        [HttpGet]
        [Route("JobTypes")]
        public async Task<IActionResult> GetJobTypes()
        {
            var lookupModels = await this.LookupService.GetJobTypesAsync();

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }

        [HttpGet]
        [Route("CustomerTypes")]
        public async Task<IActionResult> GetCustomerTypes()
        {
            var lookupModels = await this.LookupService.GetCustomerTypesAsync();

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }

        [HttpGet]
        [Route("EmployeeTypes")]
        public async Task<IActionResult> GetEmployeeTypes()
        {
            var lookupModels = await this.LookupService.GetEmployeeTypesAsync();

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }

        [HttpGet]
        [Route("LocationTypes")]
        public async Task<IActionResult> GetLocationTypes()
        {
            var lookupModels = await this.LookupService.GetLocationTypesAsync();

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }
    }
}