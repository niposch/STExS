using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangesToGrading2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserSubmissionUserId",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubmissions_FinalSubmissionId",
                table: "UserSubmissions",
                column: "FinalSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions",
                columns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubmissions_Submissions_FinalSubmissionId",
                table: "UserSubmissions",
                column: "FinalSubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSubmissions_Submissions_FinalSubmissionId",
                table: "UserSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubmissions_FinalSubmissionId",
                table: "UserSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions");

            migrationBuilder.AddColumn<Guid>(
                name: "UserSubmissionExerciseId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserSubmissionUserId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions",
                columns: new[] { "UserId", "ExerciseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
