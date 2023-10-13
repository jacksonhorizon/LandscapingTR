using LandscapingTR.Core.Models.Domain;

namespace LandscapingTR.Core.Interfaces
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets a location by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The locations.</returns>
        Task<LocationModel> GetLocationByIdAsync(int locationId);

        /// <summary>
        /// Gets the locations in a city.
        /// </summary>
        /// <returns>The locations.</returns>
        Task<List<LocationModel>> GetLocationsByCityAsync(string city);

        /// <summary>
        /// Gets the locations in a state.
        /// </summary>
        /// <returns>The locations.</returns>
        Task<List<LocationModel>> GetLocationsByStateAsync(string state);

        /// <summary>
        /// Gets the locations by location type.
        /// </summary>
        /// <param name="locationTypeId">The location type id.</param>
        /// <returns>The locations.</returns>
        Task<List<LocationModel>> GetLocationsByLocationTypeAsync(int locationTypeId);

        /// <summary>
        /// Saves a location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The saved location.</returns>
        Task<LocationModel> SaveLocationAsync(LocationModel location);
    }
}
