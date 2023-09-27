namespace LandscapingTR.Core.Models.CompanyResources
{
    public class CustomerModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the customer type id.
        /// </summary>
        public int CustomerTypeId { get; set; }
    }
}
