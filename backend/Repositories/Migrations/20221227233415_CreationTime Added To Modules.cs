using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class CreationTimeAddedToModules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ParsonSolutions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ParsonExercises");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ParsonElements");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Chapters");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Modules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Modules");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WeatherForecasts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ParsonSolutions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ParsonExercises",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ParsonElements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Modules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Chapters",
                type: "datetime2",
                nullable: true);
        }
    }
}
