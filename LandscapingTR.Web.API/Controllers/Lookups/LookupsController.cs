using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService LookupService;

        public LookupsController(ILookupService lookupService)
        {
            this.LookupService = lookupService;
        }

        [HttpGet]
        [Route("GetJobTypes")]
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
        [Route("GetCustomerTypes")]
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
        [Route("GetEmployeeTypes")]
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
        [Route("GetLocationTypes")]
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