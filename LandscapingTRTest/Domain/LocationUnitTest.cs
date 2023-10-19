using System.Transactions;
using AutoMapper;
using LandscapingTR.Core;
using LandscapingTR.Core.Enums.Lookups;
using LandscapingTR.Core.Factories;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Domain;
using LandscapingTR.Core.Services;
using LandscapingTR.Infrastructure;
using LandscapingTR.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Test.Time
{
    [TestClass]
    public class LocationUnitTest
    {

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
        public async Task Location_SaveNewLocation_Succeeds()
        {
            var savedLocationqModel = await AddNewLocationModelAsync((int)LocationTypes.PublicAndInstitutional, "Tucson", "Arizona");
        }

        [TestMethod]
        public async Task Location_SaveNewLocations_Succeeds()
        {
            var savedJobModel = await AddNewLocationModelAsync((int)LocationTypes.PublicAndInstitutional, "Tucson", "Arizona");
            savedJobModel = await AddNewLocationModelAsync((int)LocationTypes.CommercialAndBusiness, "Mesa", "Arizona");
            savedJobModel = await AddNewLocationModelAsync((int)LocationTypes.ResidentialAndCommunity, "Scottsdale", "Arizona");
        }

        [TestMethod]
        public async Task Location_GetLocationsByLocationTypes_Succeeds()
        {
            // Add locations.
            var savedLocationModelOne = await AddNewLocationModelAsync((int)LocationTypes.EventAndEntertainment, "Tucson", "Arizona");
            var savedLocationModelTwo = await AddNewLocationModelAsync((int)LocationTypes.EventAndEntertainment, "Tucson", "Arizona");
            var savedLocationModelThree = await AddNewLocationModelAsync((int)LocationTypes.ResidentialAndCommunity, "Scottsdale", "Arizona");

            var locationsOfTypeEventAndEntertainment = await LocationService.GetLocationsByLocationTypeAsync((int)LocationTypes.EventAndEntertainment);
            Assert.AreEqual(2, locationsOfTypeEventAndEntertainment.Count);
        }

        [TestMethod]
        public async Task Location_GetLocationsByCity_Succeeds()
        {
            // Add locations.
            var savedLocationModelOne = await AddNewLocationModelAsync((int)LocationTypes.ResidentialAndCommunity, "Tucson", "Arizona");
            var savedLocationModelTwo = await AddNewLocationModelAsync((int)LocationTypes.EventAndEntertainment, "Scottsdale", "Arizona");
            var savedLocationModelThree = await AddNewLocationModelAsync((int)LocationTypes.PublicAndInstitutional, "Scottsdale", "Arizona");

            var locationsInTucson = await LocationService.GetLocationsByCityAsync("Scottsdale");
            Assert.AreEqual(2, locationsInTucson.Count);
        }

        [TestMethod]
        public async Task Location_GetLocationsByState_Succeeds()
        {
            // Add locations.
            var savedLocationModelOne = await AddNewLocationModelAsync((int)LocationTypes.CommercialAndBusiness, "Jackson", "Wyoming");
            var savedLocationModelTwo = await AddNewLocationModelAsync((int)LocationTypes.PublicAndInstitutional, "Phoenix", "Arizona");
            var savedLocationModelThree = await AddNewLocationModelAsync((int)LocationTypes.ResidentialAndCommunity, "Cheyenne", "Wyoming");

            var locationsInArizona = await LocationService.GetLocationsByStateAsync("Wyoming");
            Assert.AreEqual(2, locationsInArizona.Count);
        }
    }
}
