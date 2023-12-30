using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeSystem.v1.Server.Migrations
{
    /// <inheritdoc />
    public partial class _1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_SchoolTrip_SchoolTripID",
                table: "Class");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_SchoolTrip_SchoolTripID",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_SchoolTripID",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolTripID",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "SchoolTripID",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "SchoolTripID",
                table: "Class");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolTripID",
                table: "Teacher",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolTripID",
                table: "Class",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SchoolTripID",
                table: "Teacher",
                column: "SchoolTripID");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SchoolTripID",
                table: "Class",
                column: "SchoolTripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_SchoolTrip_SchoolTripID",
                table: "Class",
                column: "SchoolTripID",
                principalTable: "SchoolTrip",
                principalColumn: "SchoolTripID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_SchoolTrip_SchoolTripID",
                table: "Teacher",
                column: "SchoolTripID",
                principalTable: "SchoolTrip",
                principalColumn: "SchoolTripID");
        }
    }
}
