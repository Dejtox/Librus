using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeSystem.v1.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchoolTripService : Migration
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

            migrationBuilder.CreateTable(
                name: "SchoolTripClasses",
                columns: table => new
                {
                    SchoolTripClassesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    SchoolTripID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTripClasses", x => x.SchoolTripClassesID);
                    table.ForeignKey(
                        name: "FK_SchoolTripClasses_Class_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Class",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolTripClasses_SchoolTrip_SchoolTripID",
                        column: x => x.SchoolTripID,
                        principalTable: "SchoolTrip",
                        principalColumn: "SchoolTripID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTripTeachers",
                columns: table => new
                {
                    SchoolTripTeachersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    SchoolTripID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTripTeachers", x => x.SchoolTripTeachersID);
                    table.ForeignKey(
                        name: "FK_SchoolTripTeachers_SchoolTrip_SchoolTripID",
                        column: x => x.SchoolTripID,
                        principalTable: "SchoolTrip",
                        principalColumn: "SchoolTripID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolTripTeachers_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripClasses_ClassID",
                table: "SchoolTripClasses",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripClasses_SchoolTripID",
                table: "SchoolTripClasses",
                column: "SchoolTripID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripTeachers_SchoolTripID",
                table: "SchoolTripTeachers",
                column: "SchoolTripID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolTripTeachers_TeacherID",
                table: "SchoolTripTeachers",
                column: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolTripClasses");

            migrationBuilder.DropTable(
                name: "SchoolTripTeachers");

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
