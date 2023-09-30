using AutoMapper;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;

namespace LandscapingTR.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository EmployeeRepository;

        private readonly IMapper Mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.EmployeeRepository = employeeRepository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets an employee give an employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>The employee.</returns>
        public async Task<EmployeeModel> GetEmployeeAsync(int employeeId)
        {
            var employee = await this.EmployeeRepository.GetEmployeeAsync(employeeId);
            return this.Mapper.Map<EmployeeModel>(employee);
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>The employees.</returns>
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            var employees = await this.EmployeeRepository.GetEmployeesAsync();
            return employees.Select(x => this.Mapper.Map<EmployeeModel>(x)).ToList();
        }

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>The saved employee.</returns>
        public async Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employeeModel)
        {
            if (employeeModel.Id != null)
            {
                var existingEntity = await this.EmployeeRepository.GetEmployeeAsync(employeeModel.Id.Value);

                this.Mapper.Map(employeeModel, existingEntity);

                var savedEmployee = await this.EmployeeRepository.SaveEmployeeAsync(existingEntity);

                return this.Mapper.Map<EmployeeModel>(savedEmployee);
            }
            else
            {
                var employee = this.Mapper.Map<Employee>(employeeModel);
                var savedEmployee = await this.EmployeeRepository.SaveEmployeeAsync(employee);

                return this.Mapper.Map<EmployeeModel>(savedEmployee);
            }
            
        }
    }
}
