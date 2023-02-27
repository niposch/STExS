using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class MovedIndentationIsRelevant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndentationIsRelevant",
                table: "Exercises");

            migrationBuilder.AddColumn<bool>(
                name: "IndentationIsRelevant",
                table: "ParsonSolutions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndentationIsRelevant",
                table: "ParsonSolutions");

            migrationBuilder.AddColumn<bool>(
                name: "IndentationIsRelevant",
                table: "Exercises",
                type: "bit",
                nullable: true);
        }
    }
}
