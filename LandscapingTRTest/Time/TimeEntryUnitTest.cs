using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Lookups;
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

        private static LandscapingTRDbContext Context;

        private static IMapper Mapper;


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
                context.Database.EnsureDeleted(); // Drop the existing database
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            Context = new LandscapingTRDbContext(options);

            var MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            Mapper = MapperConfig.CreateMapper();

            TimeEntryRepository = new TimeEntryRepository(Context);
            TimeEntryService = new TimeEntryService(TimeEntryRepository, Mapper);


        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            Context.Dispose();
        }

        [TestMethod]
        public async Task TimeEntry_SaveNewTimeEntry_Succeeds()
        {
            var timeEntryModel = new TimeEntryModel()
            {
                EmployeeId = 1,
                EntryDate = new DateTime(),
                EmployeeTypeId = (int)EmployeeTypes.FieldCrewWorker,
                JobTypeId = (int)JobTypes.TreeCare,
                JobId = 1,
                TotalLoggedHours = 8,
                LastModifiedDate = new DateTime(),
                IsSubmitted = false,
                IsApproved = false
            };
            
            await TimeEntryService.SaveTimeEntryAsync(timeEntryModel);

            var savedTimeEntry = (await TimeEntryService.GetSubmittedTimeEntriesByEmployeeIdAsync(1)).FirstOrDefault();

            Assert.IsNotNull(savedTimeEntry);
            //Assert.AreEqual(timeEntryModel.EmployeeId, savedTimeEntry.EmployeeId);
            Assert.AreEqual(timeEntryModel.EntryDate, savedTimeEntry.EntryDate);
        }
    }
}
