using LandscapingTRCore.Models.Lookups;

namespace LandscapingTRCore.Models.Domain
{
    public class Location
    {
        public int Id { get; set; }

        public int LocationTypeId { get; set; }

        public LocationType LocationType { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }


    }
}
