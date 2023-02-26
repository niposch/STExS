using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ModifiedTimeTrackUserSubmissionRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracks_UserSubmissions_SubmissionUserId_SubmissionExerciseId",
                table: "TimeTracks");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracks_SubmissionUserId_SubmissionExerciseId",
                table: "TimeTracks");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracks_UserId",
                table: "TimeTracks");

            migrationBuilder.DropColumn(
                name: "SubmissionExerciseId",
                table: "TimeTracks");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "TimeTracks");

            migrationBuilder.RenameColumn(
                name: "SubmissionUserId",
                table: "TimeTracks",
                newName: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_UserId_ExerciseId",
                table: "TimeTracks",
                columns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracks_UserSubmissions_UserId_ExerciseId",
                table: "TimeTracks",
                columns: new[] { "UserId", "ExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracks_UserSubmissions_UserId_ExerciseId",
                table: "TimeTracks");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracks_UserId_ExerciseId",
                table: "TimeTracks");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "TimeTracks",
                newName: "SubmissionUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionExerciseId",
                table: "TimeTracks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionId",
                table: "TimeTracks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_SubmissionUserId_SubmissionExerciseId",
                table: "TimeTracks",
                columns: new[] { "SubmissionUserId", "SubmissionExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_UserId",
                table: "TimeTracks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracks_UserSubmissions_SubmissionUserId_SubmissionExerciseId",
                table: "TimeTracks",
                columns: new[] { "SubmissionUserId", "SubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
