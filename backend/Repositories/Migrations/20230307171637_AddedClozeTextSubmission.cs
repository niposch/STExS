using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class AddedClozeTextSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClozeTextAnswerItems",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false),
                    ClozeTextSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClozeTextAnswerItems", x => new { x.ClozeTextSubmissionId, x.Index });
                    table.ForeignKey(
                        name: "FK_ClozeTextAnswerItems_Submissions_ClozeTextSubmissionId",
                        column: x => x.ClozeTextSubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClozeTextAnswerItems");
        }
    }
}
