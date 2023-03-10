using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangedParsonElementsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParsonPuzzleAnswerItems_ParsonElementId",
                table: "ParsonPuzzleAnswerItems");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonPuzzleAnswerItems_ParsonElementId",
                table: "ParsonPuzzleAnswerItems",
                column: "ParsonElementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParsonPuzzleAnswerItems_ParsonElementId",
                table: "ParsonPuzzleAnswerItems");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonPuzzleAnswerItems_ParsonElementId",
                table: "ParsonPuzzleAnswerItems",
                column: "ParsonElementId",
                unique: true);
        }
    }
}
