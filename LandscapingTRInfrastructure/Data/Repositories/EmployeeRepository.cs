using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, int?>, IEmployeeRepository
    {
        public EmployeeRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets an employee give an employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>The employee.</returns>
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var employee = await this.DataContext.Employees
                .FirstOrDefaultAsync(x => x.Id == employeeId);

            return employee;
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>The employees.</returns>
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employee = await this.DataContext.Employees
                .ToListAsync();

            return employee;
        }

        /// <summary>
        /// Saves a new employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>The saved employee.</returns>
        public async Task<Employee> SaveEmployeeAsync(Employee employee)
        {

            if (DataContext.Employees.FirstOrDefault(x => x.Id == employee.Id) != null)
            {
                // Existing employee - update it in the context
                DataContext.Employees.Update(employee);
            }
            else
            {
                // New employee - add it to the context
                DataContext.Employees.Add(employee);
            }

            await DataContext.SaveChangesAsync();

            return employee;
        }
    }
}
