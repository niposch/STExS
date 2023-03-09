using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ModifiedParsonSubmissionRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParsonElements_Submissions_ParsonPuzzleSubmissionId",
                table: "ParsonElements");

            migrationBuilder.DropIndex(
                name: "IX_ParsonElements_ParsonPuzzleSubmissionId",
                table: "ParsonElements");

            migrationBuilder.DropColumn(
                name: "ParsonPuzzleSubmissionId",
                table: "ParsonElements");

            migrationBuilder.CreateTable(
                name: "ParsonPuzzleAnswerItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParsonElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RunningNumber = table.Column<int>(type: "int", nullable: false),
                    Indentation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsonPuzzleAnswerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsonPuzzleAnswerItems_ParsonElements_ParsonElementId",
                        column: x => x.ParsonElementId,
                        principalTable: "ParsonElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParsonPuzzleAnswerItems_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParsonPuzzleAnswerItems_ParsonElementId",
                table: "ParsonPuzzleAnswerItems",
                column: "ParsonElementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParsonPuzzleAnswerItems_SubmissionId",
                table: "ParsonPuzzleAnswerItems",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParsonPuzzleAnswerItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ParsonPuzzleSubmissionId",
                table: "ParsonElements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParsonElements_ParsonPuzzleSubmissionId",
                table: "ParsonElements",
                column: "ParsonPuzzleSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParsonElements_Submissions_ParsonPuzzleSubmissionId",
                table: "ParsonElements",
                column: "ParsonPuzzleSubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");
        }
    }
}
