using LandscapingTR.Core.Enums.Lookups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAndBaseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddBaseUserAndAdmin(migrationBuilder);

        }


        private void AddBaseUserAndAdmin(MigrationBuilder migrationBuilder)
        {
            var sql = "SET IDENTITY_INSERT [Employee] ON; " +
                "DECLARE @CurrentDate DATE = CAST( GETDATE() AS DATE )" +
                "INSERT INTO [Employee] " +
                "                   ([EmployeeId],     [FirstName], [LastName], [UserName], [Password], [EmployeeTypeId], [CreatedDate])" +
                "VALUES " +
                                    $"(1,                'admin',    'admin',    'admin',    'admin',  {(int)EmployeeTypes.Administrator},      @CurrentDate)," +
                                    $"(2,                'A',          'Guy',    'I',       'dontknow',  {(int)EmployeeTypes.FieldCrewWorker},      @CurrentDate);"  +


                "SET IDENTITY_INSERT [Employee] OFF; ";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
