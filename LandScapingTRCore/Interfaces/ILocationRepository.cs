using LandscapingTR.Core.Entities.Domain;

namespace LandscapingTR.Core.Interfaces
{
    public interface ILocationRepository : IRepository<Location, int?>
    {
        /// <summary>
        /// Gets a location by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The locations.</returns>
        Task<Location> GetLocationByIdAsync(int locationId);

        /// <summary>
        /// Gets the locations in a city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>The locations.</returns>
        Task<List<Location>> GetLocationsByCityAsync(string city);

        /// <summary>
        /// Gets the locations in a state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The locations.</returns>
        Task<List<Location>> GetLocationsByStateAsync(string state);

        /// <summary>
        /// Gets the locations by location type.
        /// </summary>
        /// <param name="locationTypeId">The location type id.</param>
        /// <returns>The locations.</returns>
        Task<List<Location>> GetLocationsByLocationTypeAsync(int locationTypeId);

        /// <summary>
        /// Saves a location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The saved location.</returns>
        Task<Location> SaveLocationAsync(Location location);
    }
}
