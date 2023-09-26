using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Entities.CompanyResources;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure
{
    public class LandscapingTRDbContext : DbContext
    {
        public LandscapingTRDbContext(DbContextOptions dbContextOpitions) : base(dbContextOpitions)
        {

        }

        // Company Resources Section.

        /// <summary>
        /// The employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }


        public DbSet<Customer> Customers { get; set; }


        // Lookups Section.

        /// <summary>
        /// The difficulty types.
        /// </summary>
        public DbSet<DifficultyType> Difficulties { get; set; }

        /// <summary>
        /// The employee types.
        /// </summary>
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        /// <summary>
        /// The job types.
        /// </summary>
        public DbSet<JobType> JobTypes { get; set; }

        /// <summary>
        /// The location types.
        /// </summary>
        public DbSet<LocationType> LocationTypes { get; set; }

        /// <summary>
        /// the customer types.
        /// </summary>
        public DbSet<CustomerType> CustomerTypes { get; set; }


        // Domain Section.

        /// <summary>
        /// The jobs.
        /// </summary>
        public DbSet<Job> Jobs { get; set; }

        /// <summary>
        /// The locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }
    }
}
