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
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employeeModels = await this.EmployeeService.GetEmployeesAsync();

            if (employeeModels.Count == 0)
            {
                return BadRequest();
            }

            return Ok(employeeModels);
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            var employeeModel = await this.EmployeeService.GetEmployeeAsync(employeeId);

            if (employeeModel == null)
            {
                return BadRequest();
            }

            return Ok(employeeModel);
        }

        [HttpPost]
        [Route("Employee")]
        public async Task<IActionResult> SaveNewEmployee(EmployeeModel employeeModel)
        {
            var savedEmployeeModel = await this.EmployeeService.SaveEmployeeAsync(employeeModel);

            if (savedEmployeeModel == null)
            {
                return BadRequest();
            }

            return Ok(savedEmployeeModel);
        }

        [HttpPut]
        [Route("Employee")]
        public async Task<IActionResult> SaveEmployee(EmployeeModel employeeModel)
        {
            var savedEmployeeModel = await this.EmployeeService.SaveEmployeeAsync(employeeModel);

            if (savedEmployeeModel == null)
            {
                return BadRequest();
            }

            return Ok(savedEmployeeModel);
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