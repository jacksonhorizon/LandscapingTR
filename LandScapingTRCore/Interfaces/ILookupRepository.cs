using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Interfaces
{
    public interface ILookupRepository : IRepository<BaseLookupEntity, int?>
    {
        /// <summary>
        /// Gets the job types.
        /// </summary>
        /// <returns>The job types.</returns>
        Task<List<JobType>> GetJobTypesAsync();

        /// <summary>
        /// Gets the employee types.
        /// </summary>
        /// <returns>The employee types.</returns>
        Task<List<EmployeeType>> GetEmployeeTypesAsync();


        /// <summary>
        /// Gets the location types.
        /// </summary>
        /// <returns>The location types.</returns>
        Task<List<LocationType>> GetLocationTypesAsync();


        /// <summary>
        /// Gets the customer types.
        /// </summary>
        /// <returns>The customer types.</returns>
        Task<List<CustomerType>> GetCustomerTypesAsync();

    }
}
