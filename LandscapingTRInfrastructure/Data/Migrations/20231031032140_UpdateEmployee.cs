using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Efficiency",
                table: "Employee",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayRate",
                table: "Employee",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Efficiency",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PayRate",
                table: "Employee");
        }
    }
}
