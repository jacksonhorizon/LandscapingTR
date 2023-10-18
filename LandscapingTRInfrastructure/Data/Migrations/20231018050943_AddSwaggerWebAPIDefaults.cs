using LandscapingTR.Core.Enums.Lookups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSwaggerWebAPIDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [Employee] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Employee] " +
                "                   ([EmployeeId],     [FirstName], [LastName], [UserName], [Password], [EmployeeTypeId], [CreatedDate])" +
                "VALUES " +
                                    $"(0,                'Test',          'Test',    'Tester',       'asdwidua Dhgawdasd0w asdw',  {(int)EmployeeTypes.EquipmentAndSafetyOfficer},      @CurrentDate);" +


                "SET IDENTITY_INSERT [Employee] OFF; ";

            migrationBuilder.Sql(sql);

            sql = "SET IDENTITY_INSERT [Location] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Location] " +
                "                   ([LocationId],     [LocationTypeId], [Address], [City], [State], [CreatedDate])" +
                "VALUES " +
                                    $"(0,              1,          'Test',    'Tucson',       'Arizona', @CurrentDate);" +


                "SET IDENTITY_INSERT [Location] OFF; ";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = " DELETE FROM [Employee]; ";

            migrationBuilder.Sql(sql);

            sql = "DELETE FROM [Location]; ";

            migrationBuilder.Sql(sql);
        }
    }
}
