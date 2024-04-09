using LandscapingTR.Core.Entities.CompanyResources;

namespace LandscapingTR.Core.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, int?>
    {
        /// <summary>
        /// Gets an employee give an employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>The employee.</returns>
        Task<Employee> GetEmployeeAsync(int employeeId);

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>The employees.</returns>
        Task<List<Employee>> GetEmployeesAsync();

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>The saved employee.</returns>
        Task<Employee> SaveEmployeeAsync(Employee employee);

        Task<List<Employee>> GetAllActiveEmployeesAsync();
    }
}
