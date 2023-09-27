using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService lookupService;

        public LookupsController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }

        [HttpGet]
        [Route("JobTypes")]
        public async Task<IActionResult> GetJobTypes()
        {
            var lookupModels = await this.lookupService.GetJobTypesAsync();

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
            var lookupModels = await this.lookupService.GetCustomerTypesAsync();

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
            var lookupModels = await this.lookupService.GetEmployeeTypesAsync();

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
            var lookupModels = await this.lookupService.GetLocationTypesAsync();

            if (lookupModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(lookupModels);
        }
    }
}