using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeSystem.v1.Server.Migrations
{
    /// <inheritdoc />
    public partial class SubstituteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Substitute",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Substitute");
        }
    }
}
