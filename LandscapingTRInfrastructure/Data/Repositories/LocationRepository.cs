using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class LocationRepository : BaseRepository<Location, int?>, ILocationRepository
    {
        public LocationRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets a location by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The locations.</returns>
        public async Task<Location> GetLocationByIdAsync(int locationId)
        {
            return await this.DataContext.Locations.FirstOrDefaultAsync(x => x.Id == locationId);
        }

        /// <summary>
        /// Gets the locations in a city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>The locations.</returns>
        public async Task<List<Location>> GetLocationsByCityAsync(string city)
        {
            return await this.DataContext.Locations
                .Where(x => x.State.Equals(city))
                .ToListAsync();
        }

        /// <summary>
        /// Gets the locations by location type.
        /// </summary>
        /// <param name="locationTypeId">The location type id.</param>
        /// <returns>The locations.</returns>
        public async Task<List<Location>> GetLocationsByLocationTypeAsync(int locationTypeId)
        {
            return await this.DataContext.Locations
                .Where(x => x.LocationTypeId == locationTypeId)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the locations in a state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The locations.</returns>
        public async Task<List<Location>> GetLocationsByStateAsync(string state)
        {
            return await this.DataContext.Locations
                .Where(x => x.State.Equals(state))
                .ToListAsync();
        }

        /// <summary>
        /// Saves a location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The saved location.</returns>
        public async Task<Location> SaveLocationAsync(Location location)
        {
            await this.SaveAsync(location);
            return location;
        }
    }
}
