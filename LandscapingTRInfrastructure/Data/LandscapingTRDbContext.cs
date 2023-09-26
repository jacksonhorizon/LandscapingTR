using LandscapingTRCore.Models.Domain;
using LandscapingTRCore.Models.Lookups;
using LandscapingTRCore.Models.CompanyResources;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTRInfrastructure.Data    
{
    public class LandscapingTRDbContext: DbContext
    {
        public LandscapingTRDbContext(DbContextOptions dbContextOpitions): base(dbContextOpitions)
        {

        }

        // Lookups Section.

        /// <summary>
        /// The difficulty type.
        /// </summary>
        public DbSet<DifficultyType> Difficulties { get; set; }

        /// <summary>
        /// The employee type.
        /// </summary>
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        /// <summary>
        /// The job type.
        /// </summary>
        public DbSet<JobType> JobTypes { get; set; }

        /// <summary>
        /// The location type.
        /// </summary>
        public DbSet<LocationType> LocationTypes { get; set; }


        // Domain Section.

        /// <summary>
        /// The jobs.
        /// </summary>
        public DbSet<Job> Jobs { get; set; }

        /// <summary>
        /// The locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }


        // Company Resources Section.

        /// <summary>
        /// The employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

    }
}
