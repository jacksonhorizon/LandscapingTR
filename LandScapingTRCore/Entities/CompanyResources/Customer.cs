using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.CompanyResources
{
    public class Customer: BaseEntity<int?>
    {
        /// <summary>
        /// The customer name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer type id.
        /// </summary>
        public int CustomerTypeId { get; set; }

        /// <summary>
        /// The customer type.
        /// </summary>
        public CustomerType CustomerType { get; set; }
    }
}
