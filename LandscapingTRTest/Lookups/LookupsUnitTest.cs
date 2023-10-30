using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using LandscapingTR.Test.Time;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Lookups
{
    [TestClass]
    public class LookupsRepositoryUnitTest
    {

        private static ILookupRepository LookupRepository;

        private static ILookupService LookupService;

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

            LookupRepository = new LookupRepository(Context);
            LookupService = new LookupService(LookupRepository, Mapper);
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

        [TestMethod]
        public async Task Lookup_GetJobTypes_Succeeds()
        {
            var lookupEntities = await LookupService.GetJobTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(12, lookupEntities.Count);
        }

        [TestMethod]
        public async Task Lookup_GetLocationTypes_Succeeds()
        {
            var lookupEntities = await LookupService.GetLocationTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task Lookupy_GetEmployeeTypes_Succeeds()
        {
            var lookupEntities = await LookupService.GetEmployeeTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(5, lookupEntities.Count);
        }

        [TestMethod]
        public async Task Lookup_GetCustomerTypes_Succeeds()
        {
            var lookupEntities = await LookupService.GetCustomerTypesAsync();
            var entity = lookupEntities.FirstOrDefault();
            Assert.IsInstanceOfType(entity, typeof(LookupItemModel));
            Assert.AreEqual(3, lookupEntities.Count);
        }

        [TestMethod]
        public async Task Lookup_GetAllLookups_Succeeds()
        {
            var lookupsModel = await LookupService.GetLandscapingTRLookupsAsync();
            var locationTypes = lookupsModel.EmployeeTypes;
            var employeeTypes = lookupsModel.EmployeeTypes;
            var customerTypes = lookupsModel.CustomerTypes;
            var jobTypes = lookupsModel.JobTypes;

            Assert.IsInstanceOfType(lookupsModel, typeof(LandscapingTRLookupsModel));
            Assert.AreEqual(5, locationTypes.Count);
            Assert.AreEqual(5, employeeTypes.Count);
            Assert.AreEqual(3, customerTypes.Count);
            Assert.AreEqual(12, jobTypes.Count);
        }
    }
}
