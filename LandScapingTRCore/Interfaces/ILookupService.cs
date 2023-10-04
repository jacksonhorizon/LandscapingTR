using LandscapingTR.Core.Models;
using LandscapingTR.Core.Models.Lookups;

namespace LandscapingTR.Core.Interfaces
{
    public interface ILookupService
    {
        /// <summary>
        /// Gets the job types.
        /// </summary>
        /// <returns>The job types.</returns>
        Task<List<LookupItemModel>> GetJobTypesAsync();

        /// <summary>
        /// Gets the employee types.
        /// </summary>
        /// <returns>The employee types.</returns>
        Task<List<LookupItemModel>> GetEmployeeTypesAsync();


        /// <summary>
        /// Gets the location types.
        /// </summary>
        /// <returns>The location types.</returns>
        Task<List<LookupItemModel>> GetLocationTypesAsync();


        /// <summary>
        /// Gets the customer types.
        /// </summary>
        /// <returns>The customer types.</returns>
        Task<List<LookupItemModel>> GetCustomerTypesAsync();

        /// <summary>
        /// Gets the LandscapingTR lookups model.
        /// </summary>
        /// <returns>The lookups model.</returns>
        Task<LandscapingTRLookupsModel> GetLandscapingTRLookupsAsync();
    }
}
