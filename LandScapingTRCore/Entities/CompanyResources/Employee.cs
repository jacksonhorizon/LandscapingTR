using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.CompanyResources
{
    public class Employee: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets the employee first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the employee password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the employee type id.
        /// </summary>
        public int EmployeeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the employee type.
        /// </summary>
        public EmployeeType EmployeeType { get; set; }
    }
}
