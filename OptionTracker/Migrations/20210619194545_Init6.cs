using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TraderId",
                table: "Ticker",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Traders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true),
                    TickerSymbolId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traders_TickerSymbols_TickerSymbolId",
                        column: x => x.TickerSymbolId,
                        principalTable: "TickerSymbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TraderId",
                table: "Ticker",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_Traders_TickerSymbolId",
                table: "Traders",
                column: "TickerSymbolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticker_Traders_TraderId",
                table: "Ticker",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticker_Traders_TraderId",
                table: "Ticker");

            migrationBuilder.DropTable(
                name: "Traders");

            migrationBuilder.DropIndex(
                name: "IX_Ticker_TraderId",
                table: "Ticker");

            migrationBuilder.DropColumn(
                name: "TraderId",
                table: "Ticker");
        }
    }
}
