using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class Ticker9000 : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TickerSectorId",
                table: "Ticker",
                column: "TickerSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticker_TickerTypeId",
                table: "Ticker",
                column: "TickerTypeId");

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
