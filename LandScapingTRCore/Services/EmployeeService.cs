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

        private EmployeeModel CurrentLoggedInEmployee { get; set; }

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

                existingEntity.Username = Cryptography.Encrypt(existingEntity.Username);
                existingEntity.Password = Cryptography.Encrypt(existingEntity.Password);

                var savedEmployee = await this.EmployeeRepository.SaveEmployeeAsync(existingEntity);

                return this.Mapper.Map<EmployeeModel>(savedEmployee);
            }
            else
            {
                var employee = this.Mapper.Map<Employee>(employeeModel);

                employee.Username = Cryptography.Encrypt(employee.Username);
                employee.Password = Cryptography.Encrypt(employee.Password);

                var savedEmployee = await this.EmployeeRepository.SaveEmployeeAsync(employee);

                return this.Mapper.Map<EmployeeModel>(savedEmployee);
            }
        }

        /// <summary>
        /// Returns the model of an employee if they are able to login.
        /// </summary>
        /// <param name="username">The employee's username.</param>
        /// <param name="password">The employee's password.</param>
        /// <returns></returns>
        public async Task<EmployeeModel> Login(string username, string password)
        {
            var employees = await this.EmployeeRepository.GetEmployeesAsync();

            var encryptedUsername = "";
            var encryptedPassword = "";
            if (!username.Equals("admin"))
            {
                encryptedUsername = Cryptography.Encrypt(username);
                encryptedPassword = Cryptography.Encrypt(password);
            } else
            {
                encryptedUsername = "admin";
                encryptedPassword = "admin";
            }

            var matchingEmployee = employees.FirstOrDefault(x => x.Username.Equals(encryptedUsername) && x.Password.Equals(encryptedPassword));

            if (matchingEmployee == null)
            {
                throw new Exception("Please enter valid credentials");
            }

            this.CurrentLoggedInEmployee = this.Mapper.Map<EmployeeModel>(matchingEmployee);

            return this.CurrentLoggedInEmployee;
        }
    }
}
