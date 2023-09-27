using LandscapingTR.Core.Interfaces;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Lookups
{
    [TestClass]
    public class LookupsRepositoryUnitTest
    {

        private static ILookupRepository lookupRepository;

        private static LandscapingTRDbContext context;


        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            DbContextOptions<LandscapingTRDbContext> options;
            var builder = new DbContextOptionsBuilder<LandscapingTRDbContext>();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=LandscapingTRDb;Trusted_Connection=True;TrustServerCertificate=True;");
            options = builder.Options;
            context = new LandscapingTRDbContext(options);

            lookupRepository = new LookupRepository(context);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            // Class Cleanup
        }

        [TestMethod]
        public async Task LookupRepository_GetJobTypes_Succeeds()
        {
            var lookupEntities = await lookupRepository.GetJobTypesAsync();
            Assert.AreEqual(11, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetLocationTypes_Succeeds()
        {
            var lookupEntities = await lookupRepository.GetLocationTypesAsync();
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetEmployeeTypes_Succeeds()
        {
            var lookupEntities = await lookupRepository.GetEmployeeTypesAsync();
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetCustomerTypes_Succeeds()
        {
            var lookupEntities = await lookupRepository.GetCustomerTypesAsync();
            Assert.AreEqual(3, lookupEntities.Count);
        }
    }
}
