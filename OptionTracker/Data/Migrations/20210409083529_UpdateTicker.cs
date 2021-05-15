using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OptionTracker.Migrations
{
    public partial class UpdateTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketCap",
                table: "Ticker",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextEarnings",
                table: "Ticker",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ChartCode",
                table: "CompareRaw",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "CompareRaw",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "NextEarnings",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "ChartCode",
                table: "CompareRaw");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "CompareRaw");
        }
    }
}
