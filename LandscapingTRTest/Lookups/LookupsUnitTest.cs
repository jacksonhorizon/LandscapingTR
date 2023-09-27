using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Lookups
{
    [TestClass]
    public class LookupsRepositoryUnitTest
    {

        private static ILookupRepository lookupRepository;

        private static ILookupService lookupService;

        private static LandscapingTRDbContext context;

        private static IMapper mapper;


        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            DbContextOptions<LandscapingTRDbContext> options;
            var builder = new DbContextOptionsBuilder<LandscapingTRDbContext>();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=LandscapingTRDb;Trusted_Connection=True;TrustServerCertificate=True;");
            options = builder.Options;
            context = new LandscapingTRDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            mapper = mapperConfig.CreateMapper();

            lookupRepository = new LookupRepository(context);
            lookupService = new LookupService(lookupRepository, mapper);
        }

        [ClassCleanup]
        public static void TestCleanup()
        {
            // Class Cleanup
        }

        [TestMethod]
        public async Task LookupRepository_GetJobTypes_Succeeds()
        {
            var lookupEntities = await lookupService.GetJobTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(11, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetLocationTypes_Succeeds()
        {
            var lookupEntities = await lookupService.GetLocationTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetEmployeeTypes_Succeeds()
        {
            var lookupEntities = await lookupService.GetEmployeeTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task LookupRepository_GetCustomerTypes_Succeeds()
        {
            var lookupEntities = await lookupService.GetCustomerTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(3, lookupEntities.Count);
        }
    }
}
