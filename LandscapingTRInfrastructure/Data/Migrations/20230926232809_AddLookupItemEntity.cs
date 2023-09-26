using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "LocationTypes",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "JobTypes",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "EmployeeTypes",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Difficulties",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "CustomerTypes",
                newName: "Active");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "LocationTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "JobTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "EmployeeTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Difficulties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "CustomerTypes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "LocationTypes");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "EmployeeTypes");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Difficulties");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "CustomerTypes");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "LocationTypes",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "JobTypes",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "EmployeeTypes",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Difficulties",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "CustomerTypes",
                newName: "isActive");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
