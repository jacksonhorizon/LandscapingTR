
using LandscapingTR.Core.Enums.Lookups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddCustomerType(migrationBuilder);
            AddJobType(migrationBuilder);
            AddEmployeeType(migrationBuilder);
            AddLocationType(migrationBuilder);
            AddBaseUserAndAdmin(migrationBuilder);
        }

        private void AddCustomerType(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [CustomerType] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [CustomerType] " +
                "                   ([CustomerTypeId],            [CustomerTypeDisplayValue], [CreatedDate], [Active], [SortOrder])" +
                "VALUES " +
                                    $"({(int)CustomerTypes.Residential},   'Residential',      @CurrentDate,   1,      1)," +
                                    $"({(int)CustomerTypes.Commercial},    'Commercial',       @CurrentDate,   1,      2)," +
                                    $"({(int)CustomerTypes.Government},    'Government',       @CurrentDate,   1,      3);" +

                "SET IDENTITY_INSERT [CustomerType] OFF; ";

            migrationBuilder.Sql(sql);
        }

        private void AddJobType(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [JobType] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [JobType] " +
                "                   ([JobTypeId],                                     [JobTypeDisplayValue],               [CreatedDate], [Active], [SortOrder])" +
                "VALUES " +
                                    $"({(int)JobTypes.RoutineMaintenance},            'Routine Maintenance',                @CurrentDate,   1,      1)," +
                                    $"({(int)JobTypes.IntermediateMaintenace},        'Intermediate Maintenace',            @CurrentDate,   1,      2)," +
                                    $"({(int)JobTypes.HardscapingAndConstruction},    'Hardscaping And Construction',       @CurrentDate,   1,      3)," +
                                    $"({(int)JobTypes.LandscapeDesign},               'Landscape Design',                   @CurrentDate,   1,      4)," +
                                    $"({(int)JobTypes.TreeCare},                      'Tree Care',                          @CurrentDate,   1,      5)," +
                                    $"({(int)JobTypes.WaterManagement},               'Water Management',                   @CurrentDate,   1,      6)," +
                                    $"({(int)JobTypes.EnvironmentalLandscaping},      'Environmental Landscaping',          @CurrentDate,   1,      7)," +
                                    $"({(int)JobTypes.ErosionControlAndRestoration},  'Erosion Control And Restoration',    @CurrentDate,   1,      8)," +
                                    $"({(int)JobTypes.CommercialLandscaping},         'Commercial Landscaping',             @CurrentDate,   1,      9)," +
                                    $"({(int)JobTypes.SeasonalLandscaping},           'Seasonal Landscaping',               @CurrentDate,   1,      10)," +
                                    $"({(int)JobTypes.ArtisticLandscaping},           'ArtisticLandscaping',                @CurrentDate,   1,      11)," +
                                    $"({(int)JobTypes.Administration},                'Administration',                     @CurrentDate,   1,      12);" +

                "SET IDENTITY_INSERT [JobType] OFF; ";

            migrationBuilder.Sql(sql);
        }

        private void AddEmployeeType(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [EmployeeType] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [EmployeeType] " +
                "                   ([EmployeeTypeId],                                    [EmployeeTypeDisplayValue],       [CreatedDate], [Active], [SortOrder])" +
                "VALUES " +
                                $"({(int)EmployeeTypes.FieldCrewWorker},              'Field Crew Worker',               @CurrentDate,   1,      1)," +
                                $"({(int)EmployeeTypes.CrewSupervisor},               'Crew Supervisor',                 @CurrentDate,   1,      2)," +
                                $"({(int)EmployeeTypes.LandscapeDesigner},            'Landscape Designer',              @CurrentDate,   1,      3)," +
                                $"({(int)EmployeeTypes.Administrator},                'Administrator',                   @CurrentDate,   1,      4)," +
                                $"({(int)EmployeeTypes.EquipmentAndSafetyOfficer},    'EquipmentAndSafetyOfficer',       @CurrentDate,   1,      5);" +

                "SET IDENTITY_INSERT [EmployeeType] OFF; ";

            migrationBuilder.Sql(sql);
        }

        private void AddLocationType(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [LocationType] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [LocationType] " +
                "                   ([LocationTypeId],                                       [LocationTypeDisplayValue],             [CreatedDate], [Active], [SortOrder])" +
                "VALUES " +
                                    $"({(int)LocationTypes.ResidentialAndCommunity},         'Residential And Community',             @CurrentDate,   1,      1)," +
                                    $"({(int)LocationTypes.CommercialAndBusiness},           'Commercial And Business',               @CurrentDate,   1,      2)," +
                                    $"({(int)LocationTypes.PublicAndInstitutional},          'Public And Institutional',              @CurrentDate,   1,      3)," +
                                    $"({(int)LocationTypes.EventAndEntertainment},           'Event And Entertainment',               @CurrentDate,   1,      4)," +
                                    $"({(int)LocationTypes.TransportationAndGreenSpaces},    'Transportation And Green Spaces',       @CurrentDate,   1,      5);" +
                      "SET IDENTITY_INSERT [LocationType] OFF; ";

            migrationBuilder.Sql(sql);

        }
        private void AddBaseUserAndAdmin(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [Employee] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Employee] " +
                "                   ([EmployeeId],     [FirstName], [LastName], [UserName], [Password], [EmployeeTypeId], [CreatedDate])" +
                "VALUES " +
                                    $"(1,                'admin',    'admin',    'admin',    'admin',  {(int)EmployeeTypes.Administrator},      @CurrentDate);" +


                "SET IDENTITY_INSERT [Employee] OFF; ";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = " DELETE FROM [CustomerType]; ";

            migrationBuilder.Sql(sql);

            sql = "DELETE FROM [JobType]; ";

            migrationBuilder.Sql(sql);

            sql = "DELETE FROM [EmployeeType]; ";

            migrationBuilder.Sql(sql);

            sql = "DELETE FROM [LocationType];";

            migrationBuilder.Sql(sql);

            sql = "DELETE FROM [Employee]; ";

            migrationBuilder.Sql(sql);
        }
    }
}