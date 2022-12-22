using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class AnpassungModuleParticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParticipations_AspNetUsers_ModuleId",
                table: "ModuleParticipations");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleParticipations_UserId",
                table: "ModuleParticipations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParticipations_AspNetUsers_UserId",
                table: "ModuleParticipations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParticipations_AspNetUsers_UserId",
                table: "ModuleParticipations");

            migrationBuilder.DropIndex(
                name: "IX_ModuleParticipations_UserId",
                table: "ModuleParticipations");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParticipations_AspNetUsers_ModuleId",
                table: "ModuleParticipations",
                column: "ModuleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
