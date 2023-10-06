using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandscapingTR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeTimeEntryJobAndJobIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Job_JobId",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "TimeEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Job_JobId",
                table: "TimeEntry",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Job_JobId",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "TimeEntry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Job_JobId",
                table: "TimeEntry",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
