using LandscapingTR.Core.Models.CompanyResources;

namespace LandscapingTR.Core.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets an employee give an employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>The employee.</returns>
        Task<EmployeeModel> GetEmployeeAsync(int employeeId);

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>The employees.</returns>
        Task<List<EmployeeModel>> GetEmployeesAsync();

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>The saved employee.</returns>
        Task<EmployeeModel> SaveEmployeeAsync(EmployeeModel employeeModel);
    }
}
