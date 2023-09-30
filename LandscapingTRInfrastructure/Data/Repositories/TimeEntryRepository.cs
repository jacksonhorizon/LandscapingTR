﻿using LandscapingTR.Core.Entities.Time;
using LandscapingTR.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class TimeEntryRepository : BaseRepository<TimeEntry, int?>, ITimeEntryRepository
    {
        public TimeEntryRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets the time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntry>> GetTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await this.DataContext.TimeEntries
                .Where(x => x.EmployeeId == employeeId &&
                    (startDate == null || endDate == null) ? (x.EntryDate > startDate && x.EntryDate < endDate) : true)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the submitted time entries by employee id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntry>> GetSubmittedTimeEntriesByEmployeeIdAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await this.DataContext.TimeEntries
                .Where(x => x.EmployeeId == employeeId && 
                    ((startDate == null || endDate == null) ? (x.EntryDate > startDate && x.EntryDate < endDate) : true)
                    && x.IsSubmitted == true)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the time entries in a date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntry>> GetTimeEntriesByDateRange(DateTime? startDate, DateTime? endDate)
        {
            return await this.DataContext.TimeEntries
                .Where(x => x.EntryDate < endDate && x.EntryDate > startDate)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the time entries by jobe type.
        /// </summary>
        /// <param name="jobTypeId">The job type id.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntry>> GetTimeEntriesByJobType(int jobTypeId)
        {
            return await this.DataContext.TimeEntries
                .Include(x => x.Job)
                .Where(x => x.Job.JobTypeId == jobTypeId)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the time entries by job.
        /// </summary>
        /// <param name="jobId">The job id.</param>
        /// <returns>The time entries.</returns>
        public async Task<List<TimeEntry>> GetTimeEntriesByJobIdAsync(int jobId)
        {
            return await this.DataContext.TimeEntries
                .Where(x => x.JobId == jobId)
                .ToListAsync();
        }

        /// <summary>
        /// Saves a time entry.
        /// </summary>
        /// <param name="timeEntry">The time entry.</param>
        /// <returns>The saved time entry.</returns>
        public async Task<TimeEntry> SaveTimeEntryAsync(TimeEntry timeEntry)
        {
            this.DataContext.TimeEntries.Add(timeEntry);

            await this.DataContext.SaveChangesAsync();

            return timeEntry;
        }

        /// <summary>
        /// Saves a list of time entries.
        /// </summary>
        /// <param name="timeEntries">The time entries.</param>
        /// <returns>The saved time entry.</returns>
        public async Task<List<TimeEntry>> SaveTimeEntryRangeAsync(List<TimeEntry> timeEntries)
        {
            this.DataContext.TimeEntries.AddRange(timeEntries);

            await this.DataContext.SaveChangesAsync();

            return timeEntries;
        }
    }
}