using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Lookups;
using AutoMapper;
using LandscapingTR.Core.Models;

namespace LandscapingTR.Core.Services
{
    public class LookupService : ILookupService
    {
        private ILookupRepository LookupRepository;

        private readonly IMapper Mapper;
        public LookupService(ILookupRepository lookupRepository, IMapper mapper)
        {
            this.LookupRepository = lookupRepository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets the job types.
        /// </summary>
        /// <returns>The job types.</returns>
        public async Task<List<LookupItemModel>> GetJobTypesAsync()
        {
            var lookupEntities = await this.LookupRepository.GetJobTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                return lookupEntities.Select(x => this.Mapper.Map<LookupItemModel>(x)).ToList();
            }
        }

        /// <summary>
        /// Gets the employee types.
        /// </summary>
        /// <returns>The employee types.</returns>
        public async Task<List<LookupItemModel>> GetEmployeeTypesAsync()
        {
            var lookupEntities = await this.LookupRepository.GetEmployeeTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                return lookupEntities.Select(x => this.Mapper.Map<LookupItemModel>(x)).ToList();
            }
        }


        /// <summary>
        /// Gets the location types.
        /// </summary>
        /// <returns>The location types.</returns>
        public async Task<List<LookupItemModel>> GetLocationTypesAsync()
        {
            var lookupEntities = await this.LookupRepository.GetLocationTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                return lookupEntities.Select(x => this.Mapper.Map<LookupItemModel>(x)).ToList();
            }
        }


        /// <summary>
        /// Gets the customer types.
        /// </summary>
        /// <returns>The customer types.</returns>
        public async Task<List<LookupItemModel>> GetCustomerTypesAsync()
        {
            var lookupEntities = await this.LookupRepository.GetCustomerTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                return lookupEntities.Select(x => this.Mapper.Map<LookupItemModel>(x)).ToList();
            }
        }

        /// <summary>
        /// Gets the LandscapingTR lookups model.
        /// </summary>
        /// <returns>The lookups model.</returns>
        public async Task<LandscapingTRLookupsModel> GetLandscapingTRLookupsAsync()
        {
            var jobTypes = await this.GetJobTypesAsync();
            var employeeTypes = await this.GetEmployeeTypesAsync();
            var locationTypes = await this.GetLocationTypesAsync();
            var customerTypes = await this.GetCustomerTypesAsync();

            return new LandscapingTRLookupsModel() { 
                CustomerTypes = customerTypes,
                JobTypes = jobTypes,
                LocationTypes = locationTypes,
                EmployeeTypes = employeeTypes
            };
        }
    }
}
