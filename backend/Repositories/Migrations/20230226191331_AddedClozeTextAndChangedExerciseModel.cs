using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class AddedClozeTextAndChangedExerciseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextWithAnswers",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextWithAnswers",
                table: "Exercises");
        }
    }
}
