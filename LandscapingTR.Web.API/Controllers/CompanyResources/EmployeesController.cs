using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;

namespace LandscapingTR.Web.API.Controllers.Lookups
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService EmployeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.EmployeeService = employeeService;
        }


    }
}