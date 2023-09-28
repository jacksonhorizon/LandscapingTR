using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Entities.CompanyResources;
using Microsoft.EntityFrameworkCore;
using LandscapingTR.Core.Entities;
using LandscapingTR.Core.Entities.Time;

namespace LandscapingTR.Infrastructure
{
    public class LandscapingTRDbContext : DbContext
    {
        public LandscapingTRDbContext(DbContextOptions dbContextOpitions) : base(dbContextOpitions)
        {

        }

        // Time Section.

        /// <summary>
        /// The employees.
        /// </summary>
        public DbSet<TimeEntry> TimeEntries { get; set; }


        // Company Resources Section.

        /// <summary>
        /// The employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }


        public DbSet<Customer> Customers { get; set; }


        // Lookups Section.

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


        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            this.RenameTableAndId<TimeEntry, int?>(builder);
            this.RenameTableAndId<Employee, int?>(builder);
            this.RenameTableAndId<Customer, int?>(builder);
            this.RenameTableAndId<EmployeeType, int?>(builder);
            this.RenameTableAndId<JobType, int?>(builder);
            this.RenameTableAndId<LocationType, int?>(builder);
            this.RenameTableAndId<CustomerType, int?>(builder);

        }


        private void RenameTableAndId<TEntity, TKey>(ModelBuilder builder) where TEntity : BaseEntity<TKey>
        {
            Type type = typeof(TEntity);
            builder.Entity<TEntity>().ToTable(type.Name).Property(prop => prop.Id).HasColumnName(type.Name + "Id");
        }
    }
}
