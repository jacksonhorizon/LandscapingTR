using AutoMapper;
using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Domain;

namespace LandscapingTR.Core.Services
{
    public class LocationService : ILocationService
    {
        private ILocationRepository LocationRepository;

        private readonly IMapper Mapper;
        public LocationService(ILocationRepository LocationRepository, IMapper mapper)
        {
            this.LocationRepository = LocationRepository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets a location by location id.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <returns>The locations.</returns>
        public async Task<LocationModel> GetLocationByIdAsync(int locationId)
        {
            var location = await LocationRepository.GetLocationByIdAsync(locationId);
            return Mapper.Map<LocationModel>(location);
        }

        /// <summary>
        /// Gets the locations in a city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>The locations.</returns>
        public async Task<List<LocationModel>> GetLocationsByCityAsync(string city)
        {
            var locationModels = (await LocationRepository.GetLocationsByCityAsync(city)).Select(x => Mapper.Map<LocationModel>(x)).ToList();
            return locationModels;
        }

        /// <summary>
        /// Gets the locations by location type.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The locations.</returns>
        public async Task<List<LocationModel>> GetLocationsByStateAsync(string state)
        {
            var locationModels = (await LocationRepository.GetLocationsByStateAsync(state)).Select(x => Mapper.Map<LocationModel>(x)).ToList();
            return locationModels;
        }

        /// <summary>
        /// Gets the locations in a state.
        /// </summary>
        /// <param name="locationTypeId">The location type id.</param>
        /// <returns>The locations.</returns>
        public async Task<List<LocationModel>> GetLocationsByLocationTypeAsync(int locationTypeId)
        {
            var locationModels = (await LocationRepository.GetLocationsByLocationTypeAsync(locationTypeId)).Select(x => Mapper.Map<LocationModel>(x)).ToList();
            return locationModels;
        }

        /// <summary>
        /// Saves a location.
        /// </summary>
        /// <param name="locationModel">The location.</param>
        /// <returns>The saved location.</returns>
        public async Task<LocationModel> SaveLocationAsync(LocationModel locationModel)
        {
            var location = Mapper.Map<Location>(locationModel);
            location = await LocationRepository.SaveLocationAsync(location);

            return Mapper.Map<LocationModel>(location);
        }
    }
}
