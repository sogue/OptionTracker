using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OptionTracker.Migrations
{
    public partial class InitialChain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OptionContract",
                table: "OptionChain",
                newName: "OptionContracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OptionChain",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "OptionChain",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnderlyingPrice",
                table: "OptionChain",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "OptionChain");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "OptionChain");

            migrationBuilder.DropColumn(
                name: "UnderlyingPrice",
                table: "OptionChain");

            migrationBuilder.RenameColumn(
                name: "OptionContracts",
                table: "OptionChain",
                newName: "OptionContract");
        }
    }
}
