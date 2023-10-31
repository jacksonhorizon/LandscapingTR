using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Domain;
using LandscapingTR.Core.Models.Time;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Time
{
    [TestClass]
    public class TimeEntryUnitTest
    {

        private static ITimeEntryRepository TimeEntryRepository;

        private static ITimeEntryService TimeEntryService;

        private static ITimeEntryHistoryRepository TimeEntryHistoryRepository;

        private static ITimeEntryHistoryService TimeEntryHistoryService;

        private static IEmployeeRepository EmployeeRepository;

        private static IEmployeeService EmployeeService;

        private static IJobRepository JobRepository;

        private static IJobService JobService;

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

            TimeEntryHistoryRepository = new TimeEntryHistoryRepository(Context);
            TimeEntryHistoryService = new TimeEntryHistoryService(TimeEntryHistoryRepository, JobRepository, Mapper);

            TimeEntryRepository = new TimeEntryRepository(Context);
            TimeEntryService = new TimeEntryService(TimeEntryRepository, TimeEntryHistoryRepository, JobRepository, Mapper);

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
        private async Task<EmployeeModel> AddNewEmployeeAsync()
        {
            // Add a new employee.
            var newEmployee = new Employee()
            {
                FirstName = "Test Name",
                LastName = "Test Last Name",
                Username = "Test Username -",
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
        /// <returns>The saved job.</returns>
        private async Task<JobModel> AddNewJobModelAsync(int jobTypeId, DateTime? date = null)
        {
            // Add a new employee.
            var newJobModel = new JobModel()
            {
                JobName = "Test Name",
                JobTypeId = jobTypeId,
                EstimatedTotalHours = 8,
                TotalLoggedHours = 0,
                JobDate = date.HasValue ? date.Value : DateTime.Now,
                IsCompleted = false
            };

            var savedJobModel = await JobService.SaveJobAsync(newJobModel);

            var jobModel = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);
            Assert.IsNotNull(jobModel);

            return jobModel;
        }

        [TestMethod]
        public async Task TimeEntry_SaveNewTimeEntry_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync();

            // Add a new job.
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);

            // Save time Entry for added employee.
            var timeEntryModel = new TimeEntryModel()
            {
                EmployeeId = savedEmployeeModel.Id.Value,
                EntryDate = DateTime.Now,
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker,
                JobTypeId = (int)JobTypes.TreeCare,
                JobId = savedJobModel.Id.Value,
                TotalLoggedHours = 8,
                LastModifiedDate = DateTime.Now,
                IsSubmitted = false,
                IsApproved = false
            };
            
            var savedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            var updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(8, updatedJob.TotalLoggedHours);
            Assert.IsNotNull(savedTimeEntryModel);
            Assert.AreEqual(8, savedTimeEntryModel.TotalLoggedHours);
        }

        [TestMethod]
        public async Task TimeEntry_SaveNewSubmittedTimeEntry_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync();

            // Add a new job.
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);

            // Save time Entry for added employee.
            var timeEntryModel = new TimeEntryModel()
            {
                EmployeeId = savedEmployeeModel.Id.Value,
                EntryDate = DateTime.Now,
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker,
                JobTypeId = (int)JobTypes.TreeCare,
                JobId = savedJobModel.Id.Value,
                TotalLoggedHours = 3,
                LastModifiedDate = DateTime.Now,
                IsSubmitted = true,
                IsApproved = false
            };

            var savedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            var savedTimeEntry = (await TimeEntryService.GetTimeEntriesByEmployeeIdAsync(savedEmployeeModel.Id.Value)).FirstOrDefault();

            var updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(0, updatedJob.TotalLoggedHours);
            Assert.IsNotNull(savedTimeEntry);
            Assert.AreEqual(timeEntryModel.EntryDate, savedTimeEntry.EntryDate);
        }


        [TestMethod]
        public async Task TimeEntry_UpdateTimeEntry_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync();

            // Add a new job.
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);

            // Save time Entry for added employee.
            var timeEntryModel = new TimeEntryModel()
            {
                EmployeeId = savedEmployeeModel.Id.Value,
                EntryDate = DateTime.Now,
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker,
                JobTypeId = (int)JobTypes.TreeCare,
                JobId = savedJobModel.Id.Value,
                TotalLoggedHours = 3,
                LastModifiedDate = DateTime.Now,
                IsSubmitted = false,
                IsApproved = false
            };

            var savedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            var updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(3, updatedJob.TotalLoggedHours);
            Assert.AreEqual(3, savedTimeEntryModel.TotalLoggedHours);

            savedTimeEntryModel.TotalLoggedHours = 4;

            var updatedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(savedTimeEntryModel);

            updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(4, updatedJob.TotalLoggedHours);
            Assert.IsNotNull(savedTimeEntryModel);
            Assert.AreEqual(4, savedTimeEntryModel.TotalLoggedHours);
        }

        [TestMethod]
        public async Task TimeEntry_UpdateSubmittedTimeEntry_Succeeds()
        {
            // Add a new employee.
            var savedEmployeeModel = await AddNewEmployeeAsync();

            // Add a new job.
            var savedJobModel = await AddNewJobModelAsync((int)JobTypes.TreeCare);

            // Save time Entry for added employee.
            var timeEntryModel = new TimeEntryModel()
            {
                EmployeeId = savedEmployeeModel.Id.Value,
                EntryDate = DateTime.Now,
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker,
                JobTypeId = (int)JobTypes.TreeCare,
                JobId = savedJobModel.Id.Value,
                TotalLoggedHours = 3,
                LastModifiedDate = DateTime.Now,
                IsSubmitted = false,
                IsApproved = false
            };

            var savedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            var updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(3, updatedJob.TotalLoggedHours);
            Assert.AreEqual(3, savedTimeEntryModel.TotalLoggedHours);

            savedTimeEntryModel.TotalLoggedHours = 4;

            var updatedTimeEntryModel = await TimeEntryService.SaveTimeEntryAsync(savedTimeEntryModel);

            updatedJob = await JobService.GetJobByIdAsync(savedJobModel.Id.Value);

            Assert.AreEqual(4, updatedJob.TotalLoggedHours);
            Assert.IsNotNull(updatedTimeEntryModel);
            Assert.AreEqual(4, updatedTimeEntryModel.TotalLoggedHours);
        }
    }
}
