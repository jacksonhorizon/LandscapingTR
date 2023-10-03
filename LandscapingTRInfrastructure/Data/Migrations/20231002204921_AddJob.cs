using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrewSupervisorId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentAndSafetyOfficerId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EstimatedTotalHours",
                table: "Job",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FirstCrewMemberId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FourthCrewMemberId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobDate",
                table: "Job",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LandscapeDesignerId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondCrewMemberId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThirdCrewMemberId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalLoggedHours",
                table: "Job",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Job_LocationId",
                table: "Job",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Location_LocationId",
                table: "Job",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Location_LocationId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_Job_LocationId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CrewSupervisorId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "EquipmentAndSafetyOfficerId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "EstimatedTotalHours",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "FirstCrewMemberId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "FourthCrewMemberId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobDate",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "LandscapeDesignerId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "SecondCrewMemberId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ThirdCrewMemberId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "TotalLoggedHours",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "Job");
        }
    }
}
