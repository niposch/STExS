using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ParsonModelsAndModuleModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArchivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunningNumber = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chapters_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParsonExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunningNumber = table.Column<int>(type: "int", nullable: false),
                    AchieveablePoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsonExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsonExercises_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParsonExercises_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParsonSolutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelatedExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsonSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsonSolutions_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParsonSolutions_ParsonExercises_RelatedExerciseId",
                        column: x => x.RelatedExerciseId,
                        principalTable: "ParsonExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParsonElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelatedSolutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParsonSolutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParsonElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParsonElements_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParsonElements_ParsonSolutions_ParsonSolutionId",
                        column: x => x.ParsonSolutionId,
                        principalTable: "ParsonSolutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParsonElements_ParsonSolutions_RelatedSolutionId",
                        column: x => x.RelatedSolutionId,
                        principalTable: "ParsonSolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_OwnerId",
                table: "WeatherForecasts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_ModuleId",
                table: "Chapters",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_OwnerId",
                table: "Chapters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_OwnerId",
                table: "Modules",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonElements_OwnerId",
                table: "ParsonElements",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonElements_ParsonSolutionId",
                table: "ParsonElements",
                column: "ParsonSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonElements_RelatedSolutionId",
                table: "ParsonElements",
                column: "RelatedSolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonExercises_ChapterId",
                table: "ParsonExercises",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonExercises_OwnerId",
                table: "ParsonExercises",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonSolutions_OwnerId",
                table: "ParsonSolutions",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParsonSolutions_RelatedExerciseId",
                table: "ParsonSolutions",
                column: "RelatedExerciseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherForecasts_AspNetUsers_OwnerId",
                table: "WeatherForecasts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherForecasts_AspNetUsers_OwnerId",
                table: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "ParsonElements");

            migrationBuilder.DropTable(
                name: "ParsonSolutions");

            migrationBuilder.DropTable(
                name: "ParsonExercises");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_WeatherForecasts_OwnerId",
                table: "WeatherForecasts");
        }
    }
}
