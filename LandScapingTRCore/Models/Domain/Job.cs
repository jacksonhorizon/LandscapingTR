﻿using LandscapingTRCore.Models.Lookups;
using LandscapingTRCore.Models.CompanyResources;

namespace LandscapingTRCore.Models.Domain
{
    public class Job
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int DifficultyId { get; set; }

        public Difficulty Difficulty { get; set; }

        public int JobTypeId { get; set; }

        public JobType JobType { get; set; }
    }
}