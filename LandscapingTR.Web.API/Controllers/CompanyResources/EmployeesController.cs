using Microsoft.AspNetCore.Mvc;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;
using System.Web.Http.Cors;


namespace LandscapingTR.Web.API.Controllers.CompanyResources
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

        [HttpGet]
        [Route("AllEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employeeModels = await this.EmployeeService.GetEmployeesAsync();

            if (employeeModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(employeeModels);
        }

        [HttpPut]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var employeeModel = await this.EmployeeService.Login(loginModel.username, loginModel.password);

            if (employeeModel == null)
            {
                return BadRequest();
            }

            return Ok(employeeModel);
        }
    }
}