using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class CodeOutputAndTimeTrackChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_GradingResultId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "UserSubmissionUserId",
                table: "Submissions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserSubmissionId",
                table: "Submissions",
                newName: "ExerciseId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GradingResultId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_GradingResultId",
                table: "Submissions",
                column: "GradingResultId",
                unique: true,
                filter: "[GradingResultId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions",
                columns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_UserSubmissions_UserId_ExerciseId",
                table: "Submissions",
                columns: new[] { "UserId", "ExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_UserSubmissions_UserId_ExerciseId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_GradingResultId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Submissions",
                newName: "UserSubmissionUserId");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Submissions",
                newName: "UserSubmissionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GradingResultId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserSubmissionExerciseId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_GradingResultId",
                table: "Submissions",
                column: "GradingResultId",
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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
