using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class AddedGradingRelatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradingResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsFinal = table.Column<bool>(type: "bit", nullable: false),
                    IsAutomatic = table.Column<bool>(type: "bit", nullable: false),
                    AppealDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSubmissionUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSubmissionExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradedSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserSubmissionUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSubmissionExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradingResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionType = table.Column<int>(type: "int", nullable: false),
                    SubmittedAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_GradingResults_GradingResultId",
                        column: x => x.GradingResultId,
                        principalTable: "GradingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSubmissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubmissions", x => new { x.UserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_UserSubmissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubmissions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubmissions_Submissions_FinalSubmissionId",
                        column: x => x.FinalSubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeTracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTracks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeTracks_UserSubmissions_SubmissionUserId_SubmissionExerciseId",
                        columns: x => new { x.SubmissionUserId, x.SubmissionExerciseId },
                        principalTable: "UserSubmissions",
                        principalColumns: new[] { "UserId", "ExerciseId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradingResults_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_GradingResultId",
                table: "Submissions",
                column: "GradingResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_SubmissionUserId_SubmissionExerciseId",
                table: "TimeTracks",
                columns: new[] { "SubmissionUserId", "SubmissionExerciseId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_UserId",
                table: "TimeTracks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubmissions_ExerciseId",
                table: "UserSubmissions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubmissions_FinalSubmissionId",
                table: "UserSubmissions",
                column: "FinalSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions",
                columns: new[] { "UserSubmissionUserId", "UserSubmissionExerciseId" },
                principalTable: "UserSubmissions",
                principalColumns: new[] { "UserId", "ExerciseId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradingResults_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "GradingResults");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_UserSubmissions_UserSubmissionUserId_UserSubmissionExerciseId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "TimeTracks");

            migrationBuilder.DropTable(
                name: "UserSubmissions");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "GradingResults");
        }
    }
}
