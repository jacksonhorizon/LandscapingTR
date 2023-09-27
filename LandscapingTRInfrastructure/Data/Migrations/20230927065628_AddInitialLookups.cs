using System;
using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Enums.Lookups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Difficulties_DifficultyTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobTypes_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationTypes_LocationTypeId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_DifficultyTypeId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationTypes",
                table: "LocationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerTypes",
                table: "CustomerTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DifficultyTypeId",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "LocationTypes",
                newName: "LocationType");

            migrationBuilder.RenameTable(
                name: "JobTypes",
                newName: "JobType");

            migrationBuilder.RenameTable(
                name: "EmployeeTypes",
                newName: "EmployeeType");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "CustomerTypes",
                newName: "CustomerType");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LocationType",
                newName: "LocationTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JobType",
                newName: "JobTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployeeType",
                newName: "EmployeeTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employee",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeTypeId",
                table: "Employee",
                newName: "IX_Employee_EmployeeTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomerType",
                newName: "CustomerTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customer",
                newName: "IX_Customer_CustomerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationType",
                table: "LocationType",
                column: "LocationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobType",
                table: "JobType",
                column: "JobTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeType",
                table: "EmployeeType",
                column: "EmployeeTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerType",
                table: "CustomerType",
                column: "CustomerTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "CustomerTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employee_EmployeeId",
                table: "Jobs",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "JobTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationType_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId",
                principalTable: "LocationType",
                principalColumn: "LocationTypeId",
                onDelete: ReferentialAction.Cascade);

            AddCustomerType(migrationBuilder);
            AddJobType(migrationBuilder);
            AddEmployeeType(migrationBuilder);
            AddLocationType(migrationBuilder);
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
                                    $"({(int)JobTypes.ArtisticLandscaping},           'ArtisticLandscaping',                @CurrentDate,   1,      11);" +

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            DeleteCustomerTypes(migrationBuilder);
            DeleteJobTypes(migrationBuilder);
            DeleteEmployeeTypes(migrationBuilder);
            DeleteLocationTypes(migrationBuilder);

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerType_CustomerTypeId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Employee_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationType_LocationTypeId",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationType",
                table: "LocationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobType",
                table: "JobType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeType",
                table: "EmployeeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerType",
                table: "CustomerType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "LocationType",
                newName: "LocationTypes");

            migrationBuilder.RenameTable(
                name: "JobType",
                newName: "JobTypes");

            migrationBuilder.RenameTable(
                name: "EmployeeType",
                newName: "EmployeeTypes");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "CustomerType",
                newName: "CustomerTypes");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "LocationTypeId",
                table: "LocationTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "JobTypeId",
                table: "JobTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeTypeId",
                table: "EmployeeTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_EmployeeTypeId",
                table: "Employees",
                newName: "IX_Employees_EmployeeTypeId");

            migrationBuilder.RenameColumn(
                name: "CustomerTypeId",
                table: "CustomerTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customers",
                newName: "IX_Customers_CustomerTypeId");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyTypeId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationTypes",
                table: "LocationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTypes",
                table: "EmployeeTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerTypes",
                table: "CustomerTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DifficultTypeDispalyValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DifficultyTypeId",
                table: "Jobs",
                column: "DifficultyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerTypes_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId",
                principalTable: "CustomerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
                table: "Employees",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Difficulties_DifficultyTypeId",
                table: "Jobs",
                column: "DifficultyTypeId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Employees_EmployeeId",
                table: "Jobs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobTypes_JobTypeId",
                table: "Jobs",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationTypes_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId",
                principalTable: "LocationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        private void DeleteCustomerTypes(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [CustomerType]";

            migrationBuilder.Sql(sql);
        }

        private void DeleteJobTypes(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [jobType]";

            migrationBuilder.Sql(sql);
        }

        private void DeleteEmployeeTypes(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [EmployeeType]";

            migrationBuilder.Sql(sql);
        }

        private void DeleteLocationTypes(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM [LocationType]";

            migrationBuilder.Sql(sql);
        }
    }
}
