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
            addDiffcultyType(migrationBuilder);
            addEmployeeType(migrationBuilder);
            addJobType(migrationBuilder);
            addLocationType(migrationBuilder);
        }

        private void addDiffcultyType(MigrationBuilder migrationBuilder)
        {
            var sql = "";


            migrationBuilder.Sql(sql);
        }
        private void addEmployeeType(MigrationBuilder migrationBuilder)
        {
            var sql = "";


            migrationBuilder.Sql(sql);
        }

        private void addJobType(MigrationBuilder migrationBuilder)
        {
            var sql = "";


            migrationBuilder.Sql(sql);
        }

        private void addLocationType(MigrationBuilder migrationBuilder)
        {
            var sql = "";


            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
