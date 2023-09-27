using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class LookupRepository: BaseRepository<BaseLookupEntity, int?>, ILookupRepository
    {
        public LookupRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the job types.
        /// </summary>
        /// <returns>The job types.</returns>
        public async Task<List<JobType>> GetJobTypesAsync()
        {
            return await this.DataContext.JobTypes.AsNoTracking().ToListAsync();
            
        }

        /// <summary>
        /// Gets the employee types.
        /// </summary>
        /// <returns>The employee types.</returns>
        public async Task<List<EmployeeType>> GetEmployeeTypesAsync()
        {
            return await this.DataContext.EmployeeTypes.AsNoTracking().ToListAsync();

        }

        /// <summary>
        /// Gets the location types.
        /// </summary>
        /// <returns>The location types.</returns>
        public async Task<List<LocationType>> GetLocationTypesAsync()
        {
            return await this.DataContext.LocationTypes.AsNoTracking().ToListAsync();

        }

        /// <summary>
        /// Gets the customer types.
        /// </summary>
        /// <returns>The customer types.</returns>
        public async Task<List<CustomerType>> GetCustomerTypesAsync()
        {
            return await this.DataContext.CustomerTypes.AsNoTracking().ToListAsync();

        }
    }
}
