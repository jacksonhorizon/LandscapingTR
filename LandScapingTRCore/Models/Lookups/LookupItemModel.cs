﻿namespace LandscapingTR.Core.Models.Lookups
{
    public class LookupItemModel
    {
        public int? Id { get; set; }

        public string LookupValue { get; set; }

        public int? SortOrder { get; set; }

        public bool Active { get; set; }
    }
}
