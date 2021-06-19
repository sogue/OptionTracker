using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TickerSectorId",
                table: "Ticker",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TickerTypeId",
                table: "Ticker",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TickerSectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerSectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TickerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TickerTrader",
                columns: table => new
                {
                    TickersId = table.Column<int>(type: "integer", nullable: false),
                    TradersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerTrader", x => new { x.TickersId, x.TradersId });
                    table.ForeignKey(
                        name: "FK_TickerTrader_Ticker_TickersId",
                        column: x => x.TickersId,
                        principalTable: "Ticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TickerTrader_Traders_TradersId",
                        column: x => x.TradersId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TickerSectorId",
                table: "Ticker",
                column: "TickerSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TickerTypeId",
                table: "Ticker",
                column: "TickerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TickerTrader_TradersId",
                table: "TickerTrader",
                column: "TradersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticker_TickerSectors_TickerSectorId",
                table: "Ticker",
                column: "TickerSectorId",
                principalTable: "TickerSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticker_TickerTypes_TickerTypeId",
                table: "Ticker",
                column: "TickerTypeId",
                principalTable: "TickerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticker_TickerSectors_TickerSectorId",
                table: "Ticker");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticker_TickerTypes_TickerTypeId",
                table: "Ticker");

            migrationBuilder.DropTable(
                name: "TickerSectors");

            migrationBuilder.DropTable(
                name: "TickerTrader");

            migrationBuilder.DropTable(
                name: "TickerTypes");

            migrationBuilder.DropTable(
                name: "Traders");

            migrationBuilder.DropIndex(
                name: "IX_Ticker_TickerSectorId",
                table: "Ticker");

            migrationBuilder.DropIndex(
                name: "IX_Ticker_TickerTypeId",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "TickerSectorId",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "TickerTypeId",
                table: "Ticker");
        }
    }
}
