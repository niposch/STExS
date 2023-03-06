using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangesForParsonSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
