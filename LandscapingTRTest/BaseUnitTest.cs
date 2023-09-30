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
    public class BaseUnitTest
    {
        private static ILookupRepository LookupRepository;

        private static ILookupService LookupService;

        private static ITimeEntryRepository TimeEntryRepository;

        private static ITimeEntryService TimeEntryService;

        private static IEmployeeRepository EmployeeRepository;

        private static IEmployeeService EmployeeService;

        private static LandscapingTRDbContext Context;

        private static IMapper Mapper;

        private TransactionScope TransactionScope;

        [AssemblyInitialize]
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

            EmployeeRepository = new EmployeeRepository(Context);
            EmployeeService = new EmployeeService(EmployeeRepository, Mapper);
        }

        [AssemblyCleanup]
        public static void ClassCleanup()
        {
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
    }
}
