using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LandscapingTR.Test.Lookups
{
    [TestClass]
    public class EmployeeUnitTest
    {

        private static IEmployeeRepository EmployeeRepository;

        private static IEmployeeService EmployeeService;

        private static LandscapingTRDbContext Context;

        private static IMapper Mapper;

        private TransactionScope TransactionScope;


        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            DbContextOptions<LandscapingTRDbContext> options;
            var builder = new DbContextOptionsBuilder<LandscapingTRDbContext>();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=LandscapingTRDb.Test;Trusted_Connection=True;TrustServerCertificate=True;");
            options = builder.Options;

            // Create the database if it doesn't exist
            using (var context = new LandscapingTRDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            Context = new LandscapingTRDbContext(options);

            var MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            Mapper = MapperConfig.CreateMapper();

            EmployeeRepository = new EmployeeRepository(Context);
            EmployeeService = new EmployeeService(EmployeeRepository, Mapper);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Class Cleanup
            Context.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.TransactionScope = TransactionScopeFactory.createReadUncommitted();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TransactionScope.Dispose();
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <returns>The saved employee.</returns>
        private async Task<EmployeeModel> AddNewEmployee(string firstName)
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = firstName,
                LastName = "Test Last Name",
                Password = "Test Password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            return savedEmployeeModel;
        }

        [TestMethod]
        public async Task Employee_SaveNewEmployee_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployee("Employee 1");
            Assert.IsNotNull(savedEmployeeModel);
        }

        [TestMethod]
        public async Task Employee_GetEmployee_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployee("Employee 2");
            Assert.IsNotNull(savedEmployeeModel);

            var newSavedEmployeeModel = await EmployeeService.GetEmployeeAsync(savedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);
            Assert.AreEqual(savedEmployeeModel.Id.Value, newSavedEmployeeModel.Id.Value);
        }

        [TestMethod]
        public async Task Employee_UpdateEmployee_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployee("Employee 3");

            // Update the employee.
            savedEmployeeModel.EmployeeTypeId = (int)EmployeeTypes.CrewSupervisor;

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(savedEmployeeModel);

            savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);
            Assert.AreEqual(savedEmployeeModel.EmployeeTypeId, (int)EmployeeTypes.CrewSupervisor);
        }

        [TestMethod]
        public async Task Employee_GetEmployees_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModelOne = await AddNewEmployee("Employee 4");

            // Add a new employee.
            var savedEmployeeModelTwo = await AddNewEmployee("Employee 5");

            // Add a new employee.
            var savedEmployeeModelThree = await AddNewEmployee("Employee 6");

            // Add a new employee.
            var savedEmployeeModelFour = await AddNewEmployee("Employee 7");

            // Add a new employee.
            var savedEmployeeModelFive = await AddNewEmployee("Employee 8");


            var savedEmployeeModels = await EmployeeService.GetEmployeesAsync();
            Assert.IsNotNull(savedEmployeeModels);
            Assert.AreEqual(5, savedEmployeeModels.Count);
        }
    }
}
