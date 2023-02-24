using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangesToGrading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSubmissions_Submissions_FinalSubmissionId",
                table: "UserSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_UserSubmissions_FinalSubmissionId",
                table: "UserSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions");
            migrationBuilder.Sql("update UserSubmissions set FinalSubmissionId = NULL;");
            migrationBuilder.Sql("TRUNCATE TABLE Submissions");

            migrationBuilder.DropColumn(
                name: "IsAutomatic",
                table: "GradingResults");

            migrationBuilder.DropColumn(
                name: "UserSubmissionId",
                table: "GradingResults");

            migrationBuilder.RenameColumn(
                name: "IsFinal",
                table: "GradingResults",
                newName: "IsAutomaticallyGraded");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubmissionUserId",
                table: "GradingResults",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubmissionExerciseId",
                table: "GradingResults",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalAppealDate",
                table: "GradingResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GradingState",
                table: "GradingResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults");

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

            migrationBuilder.DropColumn(
                name: "FinalAppealDate",
                table: "GradingResults");

            migrationBuilder.DropColumn(
                name: "GradingState",
                table: "GradingResults");

            migrationBuilder.RenameColumn(
                name: "IsAutomaticallyGraded",
                table: "GradingResults",
                newName: "IsFinal");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubmissionUserId",
                table: "GradingResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserSubmissionExerciseId",
                table: "GradingResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutomatic",
                table: "GradingResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserSubmissionId",
                table: "GradingResults",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserSubmissions_FinalSubmissionId",
                table: "UserSubmissions",
                column: "FinalSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId_ExerciseId",
                table: "Submissions",
                columns: new[] { "UserId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSubmissions_Submissions_FinalSubmissionId",
                table: "UserSubmissions",
                column: "FinalSubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");
        }
    }
}
