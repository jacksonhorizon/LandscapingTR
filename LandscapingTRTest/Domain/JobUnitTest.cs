using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Domain;
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

        private static ILocationRepository LocationRepository;

        private static ILocationService LocationService;

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

            LocationRepository = new LocationRepository(Context);
            LocationService = new LocationService(LocationRepository, Mapper);
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
                Username = "Test Username",
                Password = "Test Password",
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker
            };
            var newEmployeeModel = Mapper.Map<EmployeeModel>(newEmployee);

            var newSavedEmployeeModel = await EmployeeService.SaveEmployeeAsync(newEmployeeModel);

            var savedEmployeeModel = await EmployeeService.GetEmployeeAsync(newSavedEmployeeModel.Id.Value);
            Assert.IsNotNull(savedEmployeeModel);

            return savedEmployeeModel;
        }

        /// <summary>
        /// Adds a new job.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <param name="date">The date of the job.</param>
        /// <param name="locationId">The location id.</param>
        /// <returns>The saved job.</returns>
        private async Task<JobModel> AddNewJobModelAsync(int jobTypeId, DateTime? date = null, int? locationId = null)
        {
            // Add a new job.
            var newJobModel = new JobModel()
            {
                JobTypeId = jobTypeId,
                EstimatedTotalHours = 8,
                TotalLoggedHours = 0,
                JobDate = date.HasValue ? date.Value : DateTime.Now,
                isCompleted = false,
                LocationId = locationId
            };

            var savedJobModel = await JobService.SaveJobAsync(newJobModel);

            var jobModel = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);
            Assert.IsNotNull(jobModel);

            return jobModel;
        }

        /// <summary>
        /// Adds a new location.
        /// </summary>
        /// <param name="locationTypeId">The location type id.</param>
        /// <returns>The saved location.</returns>
        private async Task<LocationModel> AddNewLocationModelAsync(int locationTypeId, string city, string state)
        {
            // Add a new location.
            var newLocationModel = new LocationModel()
            {
                LocationTypeId = locationTypeId,
                Address = "15332 Does Not Exist St",
                City = city,
                State = state,
            };

            var savedLocationModel = await LocationService.SaveLocationAsync(newLocationModel);

            var locationModel = await LocationService.GetLocationByIdAsync(savedLocationModel.Id.Value);
            Assert.IsNotNull(locationModel);

            return locationModel;
        }

        [TestMethod]
        public async Task Job_SaveNewJob_Succeeds()
        {
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);
        }

        [TestMethod]
        public async Task Job_SaveNewJobs_Succeeds()
        {
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);
            savedJobModel = await AddNewJobModelAsync((int)JobTypes.LandscapeDesign);
            savedJobModel = await AddNewJobModelAsync((int)JobTypes.WaterManagement);
        }

        [TestMethod]
        public async Task Job_AssignFirstCrewmember_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync("Employee 1");

            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);

            var updatedJobModel = await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModel.Id.Value);

            Assert.AreEqual(updatedJobModel.FirstCrewMemberId, savedEmployeeModel.Id.Value);
        }

        [TestMethod]
        public async Task Job_AssignFourCrewMembers_Succeeds()
        {
            // Add employees.
            var savedEmployeeModelOne = await AddNewEmployeeAsync("Employee 1");
            var savedEmployeeModelTwo = await AddNewEmployeeAsync("Employee 2");
            var savedEmployeeModelThree = await AddNewEmployeeAsync("Employee 3");
            var savedEmployeeModelFour = await AddNewEmployeeAsync("Employee 4");

            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.WaterManagement);

            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelOne.Id.Value, savedJobModel.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelTwo.Id.Value, savedJobModel.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelThree.Id.Value, savedJobModel.Id.Value);
            var updatedJobModel = await JobService.AssignEmployeeToJobAsync(savedEmployeeModelFour.Id.Value, savedJobModel.Id.Value);

            Assert.AreEqual(updatedJobModel.FirstCrewMemberId, savedEmployeeModelOne.Id.Value);
            Assert.AreEqual(updatedJobModel.SecondCrewMemberId, savedEmployeeModelTwo.Id.Value);
            Assert.AreEqual(updatedJobModel.ThirdCrewMemberId, savedEmployeeModelThree.Id.Value);
            Assert.AreEqual(updatedJobModel.FourthCrewMemberId, savedEmployeeModelFour.Id.Value);
        }

        [TestMethod]
        public async Task Job_GetJobsByJobTypes_Succeeds()
        {
            // Add employees.
            var savedEmployeeModelOne = await AddNewEmployeeAsync("Employee 1");
            var savedEmployeeModelTwo = await AddNewEmployeeAsync("Employee 2");
            var savedEmployeeModelThree = await AddNewEmployeeAsync("Employee 3");
            var savedEmployeeModelFour = await AddNewEmployeeAsync("Employee 4");

            // Save jobs
            var savedJobModelOne = await AddNewJobModelAsync((int)JobTypes.WaterManagement);
            var savedJobModelTwo = await AddNewJobModelAsync((int)JobTypes.WaterManagement);
            var savedJobModelThree = await AddNewJobModelAsync((int)JobTypes.WaterManagement);
            var savedJobModelFour = await AddNewJobModelAsync((int)JobTypes.WaterManagement);

            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelOne.Id.Value, savedJobModelOne.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelTwo.Id.Value, savedJobModelTwo.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelThree.Id.Value, savedJobModelThree.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModelFour.Id.Value, savedJobModelFour.Id.Value);


            var jobsOfTypeWaterManagement = await JobService.GetJobsByJobTypeAsync((int)JobTypes.WaterManagement);
            Assert.AreEqual(4, jobsOfTypeWaterManagement.Count);
        }

        [TestMethod]
        public async Task Job_GetJobsByDateRange_Succeeds()
        {
            // Save jobs
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 10));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 11));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 12));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 13));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 14));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 15));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 9, 24));

            // Save jobs outside of search range.
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 8, 29));
            await AddNewJobModelAsync((int)JobTypes.WaterManagement, new DateTime(2023, 8, 30));

            var jobsOfTypeWaterManagement = await JobService.GetJobsByDateRangeAsync(new DateTime(2023, 9, 1), DateTime.Now);
            Assert.AreEqual(7, jobsOfTypeWaterManagement.Count());
        }

        [TestMethod]
        public async Task Job_GetJobsByEmployeeId_Succeeds()
        {
            // Add employee.
            var savedEmployeeModel = await AddNewEmployeeAsync("Employee 1");

            // Save jobs
            var savedJobModelOne = await AddNewJobModelAsync((int)JobTypes.WaterManagement);
            var savedJobModelTwo = await AddNewJobModelAsync((int)JobTypes.RoutineMaintenance);
            var savedJobModelThree = await AddNewJobModelAsync((int)JobTypes.LandscapeDesign);
            var savedJobModelFour = await AddNewJobModelAsync((int)JobTypes.ArtisticLandscaping);
            var savedJobModelFive = await AddNewJobModelAsync((int)JobTypes.WaterManagement);

            await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModelOne.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModelTwo.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModelThree.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModelFour.Id.Value);
            await JobService.AssignEmployeeToJobAsync(savedEmployeeModel.Id.Value, savedJobModelFive.Id.Value);

            var jobsAssignedToEmployeeModel = await JobService.GetJobsByEmployeeIdAsync(savedEmployeeModel.Id.Value);
            Assert.AreEqual(5, jobsAssignedToEmployeeModel.Count);
        }

        [TestMethod]
        public async Task Job_GetJobsByLocationId_Succeeds()
        {
            var savedLocationModelOne = await AddNewLocationModelAsync((int)LocationTypes.PublicAndInstitutional, "Tucson", "Arizona");
            var savedLocationModelTwo = await AddNewLocationModelAsync((int)LocationTypes.CommercialAndBusiness, "Mesa", "Arizona");

            // Save jobs
            var savedJobModelOne = await AddNewJobModelAsync((int)JobTypes.WaterManagement, DateTime.Today, savedLocationModelOne.Id);
            var savedJobModelTwo = await AddNewJobModelAsync((int)JobTypes.RoutineMaintenance, DateTime.Today, savedLocationModelTwo.Id);
            var savedJobModelThree = await AddNewJobModelAsync((int)JobTypes.LandscapeDesign, DateTime.Today, savedLocationModelTwo.Id);
            var savedJobModelFour = await AddNewJobModelAsync((int)JobTypes.ArtisticLandscaping, DateTime.Today, savedLocationModelTwo.Id);
            var savedJobModelFive = await AddNewJobModelAsync((int)JobTypes.WaterManagement, DateTime.Today, savedLocationModelOne.Id);

            var jobsAssignedToLocationOne = await JobService.GetJobsByLocationIdAsync(savedLocationModelOne.Id.Value);
            var jobsAssignedToLocationTwo = await JobService.GetJobsByLocationIdAsync(savedLocationModelTwo.Id.Value);
            Assert.AreEqual(2, jobsAssignedToLocationOne.Count);
            Assert.AreEqual(3, jobsAssignedToLocationTwo.Count);
        }
    }
}
