using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class TickerHistories5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockOptionHistoryId",
                table: "OptionContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractName = table.Column<string>(type: "text", nullable: true),
                    ExpireTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOptions_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockOptionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractName = table.Column<string>(type: "text", nullable: true),
                    StockOptionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOptionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockOptionHistories_StockOptions_StockOptionId",
                        column: x => x.StockOptionId,
                        principalTable: "StockOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionContracts_StockOptionHistoryId",
                table: "OptionContracts",
                column: "StockOptionHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOptionHistories_StockOptionId",
                table: "StockOptionHistories",
                column: "StockOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StockOptions_StockId",
                table: "StockOptions",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionContracts_StockOptionHistories_StockOptionHistoryId",
                table: "OptionContracts",
                column: "StockOptionHistoryId",
                principalTable: "StockOptionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionContracts_StockOptionHistories_StockOptionHistoryId",
                table: "OptionContracts");

            migrationBuilder.DropTable(
                name: "StockOptionHistories");

            migrationBuilder.DropTable(
                name: "StockOptions");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_OptionContracts_StockOptionHistoryId",
                table: "OptionContracts");

            migrationBuilder.DropColumn(
                name: "StockOptionHistoryId",
                table: "OptionContracts");
        }
    }
}
