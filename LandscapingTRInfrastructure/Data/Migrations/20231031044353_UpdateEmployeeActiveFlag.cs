using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeActiveFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Employee",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Employee");
        }
    }
}
