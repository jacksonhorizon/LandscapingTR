using LandscapingTR.Core.Interfaces;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Factories;

namespace LandscapingTR.Core.Services
{
    public class LookupService : ILookupService
    {
        private ILookupRepository lookupRepository;

        public LookupService(ILookupRepository lookupRepository)
        {
            this.lookupRepository = lookupRepository;
        }

        /// <summary>
        /// Gets the job types.
        /// </summary>
        /// <returns>The job types.</returns>
        public async Task<List<LookupItemModel>> GetJobTypesAsync()
        {
            var lookupEntities = await this.lookupRepository.GetJobTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                //return lookupEntities.Select(x => ModelFactory.Create(x)).ToListAsync();
                return new List<LookupItemModel>();
            }
        }

        /// <summary>
        /// Gets the employee types.
        /// </summary>
        /// <returns>The employee types.</returns>
        public async Task<List<LookupItemModel>> GetEmployeeTypesAsync()
        {
            var lookupEntities = await this.lookupRepository.GetEmployeeTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                //return lookupEntities.Select(x => ModelFactory.Create(x)).ToListAsync();
                return new List<LookupItemModel>();
            }
        }


        /// <summary>
        /// Gets the location types.
        /// </summary>
        /// <returns>The location types.</returns>
        public async Task<List<LookupItemModel>> GetLocationTypesAsync()
        {
            var lookupEntities = await this.lookupRepository.GetLocationTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                //return lookupEntities.Select(x => ModelFactory.Create(x)).ToListAsync();
                return new List<LookupItemModel>();
            }
        }


        /// <summary>
        /// Gets the customer types.
        /// </summary>
        /// <returns>The customer types.</returns>
        public async Task<List<LookupItemModel>> GetCustomerTypesAsync()
        {
            var lookupEntities = await this.lookupRepository.GetCustomerTypesAsync();

            if (lookupEntities == null)
            {
                return new List<LookupItemModel>();
            }
            else
            {
                //return lookupEntities.Select(x => ModelFactory.Create(x)).ToListAsync();
                return new List<LookupItemModel>();
            }
        }
    }
}
