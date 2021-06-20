using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TickerSymbols_TickerSector_TickerSectorId",
                table: "TickerSymbols");

            migrationBuilder.DropForeignKey(
                name: "FK_TickerSymbols_TickerType_TickerTypeId",
                table: "TickerSymbols");

            migrationBuilder.DropForeignKey(
                name: "FK_Traders_TickerSymbols_TickerSymbolId",
                table: "Traders");

            migrationBuilder.DropTable(
                name: "Ticker");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropIndex(
                name: "IX_Traders_TickerSymbolId",
                table: "Traders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerType",
                table: "TickerType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerSector",
                table: "TickerSector");

            migrationBuilder.DropColumn(
                name: "TickerSymbolId",
                table: "Traders");

            migrationBuilder.RenameTable(
                name: "TickerType",
                newName: "TickerTypes");

            migrationBuilder.RenameTable(
                name: "TickerSector",
                newName: "TickerSectors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerTypes",
                table: "TickerTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerSectors",
                table: "TickerSectors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TickerSymbolTrader",
                columns: table => new
                {
                    TickersId = table.Column<int>(type: "integer", nullable: false),
                    TradersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerSymbolTrader", x => new { x.TickersId, x.TradersId });
                    table.ForeignKey(
                        name: "FK_TickerSymbolTrader_TickerSymbols_TickersId",
                        column: x => x.TickersId,
                        principalTable: "TickerSymbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TickerSymbolTrader_Traders_TradersId",
                        column: x => x.TradersId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TickerSymbolTrader_TradersId",
                table: "TickerSymbolTrader",
                column: "TradersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TickerSymbols_TickerSectors_TickerSectorId",
                table: "TickerSymbols",
                column: "TickerSectorId",
                principalTable: "TickerSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TickerSymbols_TickerTypes_TickerTypeId",
                table: "TickerSymbols",
                column: "TickerTypeId",
                principalTable: "TickerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TickerSymbols_TickerSectors_TickerSectorId",
                table: "TickerSymbols");

            migrationBuilder.DropForeignKey(
                name: "FK_TickerSymbols_TickerTypes_TickerTypeId",
                table: "TickerSymbols");

            migrationBuilder.DropTable(
                name: "TickerSymbolTrader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerTypes",
                table: "TickerTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerSectors",
                table: "TickerSectors");

            migrationBuilder.RenameTable(
                name: "TickerTypes",
                newName: "TickerType");

            migrationBuilder.RenameTable(
                name: "TickerSectors",
                newName: "TickerSector");

            migrationBuilder.AddColumn<int>(
                name: "TickerSymbolId",
                table: "Traders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerType",
                table: "TickerType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerSector",
                table: "TickerSector",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Ticker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetType = table.Column<string>(type: "text", nullable: true),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LastOptionVolume = table.Column<int>(type: "integer", nullable: false),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NextEarnings = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TraderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticker_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetType = table.Column<string>(type: "text", nullable: true),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LastOptionVolume = table.Column<int>(type: "integer", nullable: false),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NextEarnings = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traders_TickerSymbolId",
                table: "Traders",
                column: "TickerSymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TraderId",
                table: "Ticker",
                column: "TraderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TickerSymbols_TickerSector_TickerSectorId",
                table: "TickerSymbols",
                column: "TickerSectorId",
                principalTable: "TickerSector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TickerSymbols_TickerType_TickerTypeId",
                table: "TickerSymbols",
                column: "TickerTypeId",
                principalTable: "TickerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Traders_TickerSymbols_TickerSymbolId",
                table: "Traders",
                column: "TickerSymbolId",
                principalTable: "TickerSymbols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
