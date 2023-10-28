using LandscapingTR.Core.Enums.Lookups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedJobAndLocationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddBaseLocation(migrationBuilder);
            AddBaseJob(migrationBuilder);
        }

        private void AddBaseLocation(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [Location] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Location] " +
                "                   ([LocationId],         [Address],         [City],      [State],     [LocationTypeId],                          [CreatedDate])" +
                "VALUES " +
                                    $"(1,                '1040 E 4th St',    'Tucson',    'Arizona',  {(int)LocationTypes.CommercialAndBusiness},      @CurrentDate);" +


                "SET IDENTITY_INSERT [Location] OFF; ";

            migrationBuilder.Sql(sql);
        }

        private void AddBaseJob(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [Job] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Job] " +
                "                   ([JobId],         [JobName],         [JobDate],   [LocationId],     [JobTypeId],                  [EstimatedTotalHours], [TotalLoggedHours],  [isCompleted],    [CreatedDate])" +
                "VALUES " +
                                    $"(1,           'GS Landscape',    @CurrentDate,    1,      {(int)JobTypes.IntermediateMaintenace},      24,                 0,                    0x0,       @CurrentDate);" +


                "SET IDENTITY_INSERT [Location] OFF; ";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [Job];" +
                "      DELETE FROM [Location];";

            migrationBuilder.Sql(sql);
        }
    }
}
