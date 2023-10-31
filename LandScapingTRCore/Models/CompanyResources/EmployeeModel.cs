namespace LandscapingTR.Core.Models.CompanyResources
{
    public class EmployeeModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

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
        /// Gets or sets the employee created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the employee pay rate.
        /// </summary>
        public double? PayRate { get; set; }

        /// <summary>
        /// Gets or sets the employee's efficiency.
        /// </summary>
        public double? Efficiency { get; set; }
    }
}
