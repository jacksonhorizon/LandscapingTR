using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Lookups;
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
            // Class Cleanup
        }

        [TestMethod]
        public async Task LookupRepository_GetJobTypes_Succeeds()
        {
            var lookupEntities = await TimeEntryService.GetTimeEntriesByEmployeeIdAsync(1);
            Assert.AreEqual(lookupEntities.Count, 0);
        }
    }
}
