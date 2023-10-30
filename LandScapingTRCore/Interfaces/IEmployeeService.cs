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

        /// <summary>
        /// Deletes an employee.
        /// </summary>
        /// <param name="employeeId">The employee id to delete.</param>
        /// <returns>The task.</returns>
        Task<EmployeeModel> DeleteEmployeeAsync(int employeeId);

        /// <summary>
        /// Returns the model of an employee if they are able to login.
        /// </summary>
        /// <param name="username">The employee's username.</param>
        /// <param name="password">The employee's password.</param>
        /// <returns></returns>
        Task<EmployeeModel> Login(string username, string password);
    }
}
