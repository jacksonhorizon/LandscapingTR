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

        /* BEGIN TEST HEADER */

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

        /* END OF TEST HEADER */

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <returns>The saved employee.</returns>
        private async Task<EmployeeModel> AddNewEmployeeAsync(string firstName)
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = firstName,
                LastName = "Test Last Name",
                Username = "Test Username -" + firstName,
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
            var savedEmployeeModel = await AddNewEmployeeAsync("Employee 1");
            Assert.IsNotNull(savedEmployeeModel);
        }

        [TestMethod]
        public async Task Employee_GetEmployee_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync("Employee 2");
            Assert.IsNotNull(savedEmployeeModel);

            var newSavedEmployeeModel = await EmployeeService.GetEmployeeAsync(savedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);
            Assert.AreEqual(savedEmployeeModel.Id.Value, newSavedEmployeeModel.Id.Value);
        }

        [TestMethod]
        public async Task Employee_UpdateEmployee_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync("Employee 3");

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
            var savedEmployeeModelOne = await AddNewEmployeeAsync("Employee 4");

            // Add a new employee.
            var savedEmployeeModelTwo = await AddNewEmployeeAsync("Employee 5");

            // Add a new employee.
            var savedEmployeeModelThree = await AddNewEmployeeAsync("Employee 6");

            // Add a new employee.
            var savedEmployeeModelFour = await AddNewEmployeeAsync("Employee 7");

            // Add a new employee.
            var savedEmployeeModelFive = await AddNewEmployeeAsync("Employee 8");


            var savedEmployeeModels = await EmployeeService.GetEmployeesAsync();
            Assert.IsNotNull(savedEmployeeModels);

            // Includes admin, and test user
            Assert.AreEqual(7, savedEmployeeModels.Count);
        }

        [TestMethod]
        public async Task Employee_Login_Succeeds()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "LoginSucceed",
                LastName = "Test Last Name",
                Username = "potato",
                Password = "I am not sure what to put as the password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            try
            {
                var username = "potato";
                var password = "I am not sure what to put as the password";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public async Task Employee_LoginWrongUsername_Fails()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "LoginSucceed",
                LastName = "Test Last Name",
                Username = "potato",
                Password = "I am not sure what to put as the password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            try
            {
                var username = "potasdasdatos";
                var password = "I am not sure what to put as the password";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task Employee_LoginWrongPassword_Fails()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "LoginSucceed",
                LastName = "Test Last Name",
                Username = "potato",
                Password = "I am not sure what to put as the password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            try
            {
                var username = "potato";
                var password = "I am not sure what to put as the asdasdad";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task Employee_LoginAdmin_Succeeds()
        {
            try
            {
                var username = "admin";
                var password = "admin";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
                Assert.AreEqual(loggedInEmployeeModel.EmployeeTypeId, (int)EmployeeTypes.Administrator);
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public async Task Employee_Login_ChangePassword_Succeeds()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "LoginSucceed",
                LastName = "Test Last Name",
                Username = "potato",
                Password = "I am not sure what to put as the password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            savedEmployeeModel.Password = "This is the password now";

            newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(savedEmployeeModel);

            savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            try
            {
                var username = "potato";
                var password = "I am not sure what to put as the password";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
            }

            try
            {
                var username = "potato";
                var password = "This is the password now";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);   
            }
            catch (Exception e)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public async Task Employee_Login_ChangePassword_Fails()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "LoginSucceed",
                LastName = "Test Last Name",
                Username = "potato",
                Password = "I am not sure what to put as the password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            savedEmployeeModel.Password = "This is the password now";

            newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(savedEmployeeModel);

            savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            try
            {
                var username = "potato";
                var password = "I am not sure what to put as the password";
                var loggedInEmployeeModel = await EmployeeService.Login(username, password);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
