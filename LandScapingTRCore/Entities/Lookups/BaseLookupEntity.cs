namespace LandscapingTR.Core.Entities.Lookups
{
    public class BaseLookupEntity: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets whether the lookup is active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int? SortOrder { get; set; } 
    }
}
