using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.Domain
{
    public class Location: BaseEntity
    {
        /// <summary>
        /// The location type id.
        /// </summary>
        public int LocationTypeId { get; set; }

        /// <summary>
        /// The location type.
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// The location address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The location city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The location state.
        /// </summary>
        public string State { get; set; }


    }
}
