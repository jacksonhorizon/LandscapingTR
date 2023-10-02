using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Models.Time;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Time
{
    [TestClass]
    public class JobUnitTest
    {

        private static IJobRepository JobRepository;

        private static IJobService JobService;

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

            JobRepository = new JobRepository(Context);
            JobService = new JobService(JobRepository, Mapper);
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
        private async Task<EmployeeModel> AddNewEmployee()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "Test Name",
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
        public async Task Job_SaveNewJob_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployee();

            
        }
    }
}
