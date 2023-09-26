using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.Domain
{
    public class Location: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets the location type id.
        /// </summary>
        public int LocationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the location type.
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the location address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the location city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the location state.
        /// </summary>
        public string State { get; set; }


    }
}
