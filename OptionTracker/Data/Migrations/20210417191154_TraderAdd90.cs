using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class TraderAdd90 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TickerTrader_Trader_TradersId",
                table: "TickerTrader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trader",
                table: "Trader");

            migrationBuilder.RenameTable(
                name: "Trader",
                newName: "Traders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traders",
                table: "Traders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TickerTrader_Traders_TradersId",
                table: "TickerTrader",
                column: "TradersId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TickerTrader_Traders_TradersId",
                table: "TickerTrader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traders",
                table: "Traders");

            migrationBuilder.RenameTable(
                name: "Traders",
                newName: "Trader");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trader",
                table: "Trader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TickerTrader_Trader_TradersId",
                table: "TickerTrader",
                column: "TradersId",
                principalTable: "Trader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
