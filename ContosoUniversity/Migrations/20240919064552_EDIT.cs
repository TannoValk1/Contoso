using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class EDIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mood",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "WorkYears",
                table: "Instructors",
                newName: "Pay");

            migrationBuilder.RenameColumn(
                name: "VocationCredential",
                table: "Instructors",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "Pay",
                table: "Instructors",
                newName: "WorkYears");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Instructors",
                newName: "VocationCredential");

            migrationBuilder.AddColumn<int>(
                name: "Mood",
                table: "Instructors",
                type: "int",
                nullable: true);
        }
    }
}
