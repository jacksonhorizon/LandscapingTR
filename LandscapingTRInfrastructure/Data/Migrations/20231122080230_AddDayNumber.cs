using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDayNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "TimeEntryHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "TimeEntry",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "TimeEntryHistory");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "TimeEntry");
        }
    }
}
