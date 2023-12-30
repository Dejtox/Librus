using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeSystem.v1.Server.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchoolTripTeachers_SchoolTripID",
                table: "SchoolTripTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SchoolTripClasses_SchoolTripID",
                table: "SchoolTripClasses");

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
                name: "IX_SchoolTripTeachers_SchoolTripID",
                table: "SchoolTripTeachers",
                column: "SchoolTripID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripClasses_SchoolTripID",
                table: "SchoolTripClasses",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_SchoolTripTeachers_SchoolTripID",
                table: "SchoolTripTeachers");

            migrationBuilder.DropIndex(
                name: "IX_SchoolTripClasses_SchoolTripID",
                table: "SchoolTripClasses");

            migrationBuilder.DropIndex(
                name: "IX_Class_SchoolTripID",
                table: "Class");

            migrationBuilder.DropColumn(
                name: "SchoolTripID",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "SchoolTripID",
                table: "Class");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripTeachers_SchoolTripID",
                table: "SchoolTripTeachers",
                column: "SchoolTripID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripClasses_SchoolTripID",
                table: "SchoolTripClasses",
                column: "SchoolTripID",
                unique: true);
        }
    }
}
