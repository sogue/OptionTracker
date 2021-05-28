using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace OptionTracker.Migrations
{
    public partial class ComparedChain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChainResultViewModelId",
                table: "CompareRaw",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComparedChains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ticker = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ClosePrice = table.Column<float>(type: "real", nullable: false),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    TimeChange = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparedChains", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompareRaw_ChainResultViewModelId",
                table: "CompareRaw",
                column: "ChainResultViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw",
                column: "ChainResultViewModelId",
                principalTable: "ComparedChains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw");

            migrationBuilder.DropTable(
                name: "ComparedChains");

            migrationBuilder.DropIndex(
                name: "IX_CompareRaw_ChainResultViewModelId",
                table: "CompareRaw");

            migrationBuilder.DropColumn(
                name: "ChainResultViewModelId",
                table: "CompareRaw");
        }
    }
}
