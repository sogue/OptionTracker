using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class TraderAdd9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trader", x => x.Id);
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
                        name: "FK_TickerTrader_Trader_TradersId",
                        column: x => x.TradersId,
                        principalTable: "Trader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TickerTrader_TradersId",
                table: "TickerTrader",
                column: "TradersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickerTrader");

            migrationBuilder.DropTable(
                name: "Trader");
        }
    }
}
