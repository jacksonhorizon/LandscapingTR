﻿using LandscapingTR.Core.Entities.Lookups;

namespace LandscapingTR.Core.Entities.CompanyResources
{
    public class Customer: BaseEntity<int?>
    {
        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the customer type id.
        /// </summary>
        public int CustomerTypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer type.
        /// </summary>
        public CustomerType CustomerType { get; set; }
    }
}