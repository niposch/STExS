using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class AddedCodeOutputExercise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParsonExercises_AspNetUsers_OwnerId",
                table: "ParsonExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ParsonExercises_Chapters_ChapterId",
                table: "ParsonExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ParsonSolutions_ParsonExercises_RelatedExerciseId",
                table: "ParsonSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParsonExercises",
                table: "ParsonExercises");

            migrationBuilder.RenameTable(
                name: "ParsonExercises",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_ParsonExercises_OwnerId",
                table: "Exercises",
                newName: "IX_Exercises_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ParsonExercises_ChapterId",
                table: "Exercises",
                newName: "IX_Exercises_ChapterId");

            migrationBuilder.AddColumn<string>(
                name: "CorrectResponse",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseType",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExpectedAnswer",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMultiLineResponse",
                table: "Exercises",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_OwnerId",
                table: "Exercises",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Chapters_ChapterId",
                table: "Exercises",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParsonSolutions_Exercises_RelatedExerciseId",
                table: "ParsonSolutions",
                column: "RelatedExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_OwnerId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Chapters_ChapterId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ParsonSolutions_Exercises_RelatedExerciseId",
                table: "ParsonSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CorrectResponse",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseType",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExpectedAnswer",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "IsMultiLineResponse",
                table: "Exercises");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "ParsonExercises");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_OwnerId",
                table: "ParsonExercises",
                newName: "IX_ParsonExercises_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ChapterId",
                table: "ParsonExercises",
                newName: "IX_ParsonExercises_ChapterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParsonExercises",
                table: "ParsonExercises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParsonExercises_AspNetUsers_OwnerId",
                table: "ParsonExercises",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParsonExercises_Chapters_ChapterId",
                table: "ParsonExercises",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParsonSolutions_ParsonExercises_RelatedExerciseId",
                table: "ParsonSolutions",
                column: "RelatedExerciseId",
                principalTable: "ParsonExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
