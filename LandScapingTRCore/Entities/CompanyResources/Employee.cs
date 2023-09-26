using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.CompanyResources
{
    public class Employee: BaseEntity
    {
        /// <summary>
        /// The employee first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The employee last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The employee password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The employee type id.
        /// </summary>
        public int EmployeeTypeId { get; set; }

        /// <summary>
        /// The employee type.
        /// </summary>
        public EmployeeType EmployeeType { get; set; }
    }
}
