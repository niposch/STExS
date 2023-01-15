using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class CreatedCodeOutput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectResponse",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Exercises",
                newName: "ExerciseName");

            migrationBuilder.RenameColumn(
                name: "AchieveablePoints",
                table: "Exercises",
                newName: "AchievablePoints");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExerciseName",
                table: "Exercises",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "AchievablePoints",
                table: "Exercises",
                newName: "AchieveablePoints");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CorrectResponse",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
