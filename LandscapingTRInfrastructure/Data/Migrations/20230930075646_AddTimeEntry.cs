using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployeeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "Job");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Location",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Location",
                newName: "IX_Location_LocationTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Job",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Job",
                newName: "IX_Job_JobTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Job",
                table: "Job",
                column: "JobId");

            migrationBuilder.CreateTable(
                name: "TimeEntry",
                columns: table => new
                {
                    TimeEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    TotalLoggedHours = table.Column<double>(type: "float", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntry", x => x.TimeEntryId);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_EmployeeId",
                table: "TimeEntry",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_JobId",
                table: "TimeEntry",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_JobType_JobTypeId",
                table: "Job",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "JobTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_LocationType_LocationTypeId",
                table: "Location",
                column: "LocationTypeId",
                principalTable: "LocationType",
                principalColumn: "LocationTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_JobType_JobTypeId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_LocationType_LocationTypeId",
                table: "Location");

            migrationBuilder.DropTable(
                name: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Job",
                table: "Job");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Job",
                newName: "Jobs");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Locations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Location_LocationTypeId",
                table: "Locations",
                newName: "IX_Locations_LocationTypeId");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Job_JobTypeId",
                table: "Jobs",
                newName: "IX_Jobs_JobTypeId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployeeId",
                table: "Jobs",
                column: "EmployeeId");

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
        }
    }
}
