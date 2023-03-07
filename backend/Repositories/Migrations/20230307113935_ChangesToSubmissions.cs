using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class ChangesToSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAppealDate",
                table: "GradingResults");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppealableBefore",
                table: "GradingResults",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AutomaticGradingDate",
                table: "GradingResults",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManualGradingDate",
                table: "GradingResults",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppealableBefore",
                table: "GradingResults");

            migrationBuilder.DropColumn(
                name: "AutomaticGradingDate",
                table: "GradingResults");

            migrationBuilder.DropColumn(
                name: "ManualGradingDate",
                table: "GradingResults");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalAppealDate",
                table: "GradingResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
