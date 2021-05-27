using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class TickerHistories8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionChainRaw_TickerOptionHistories_TickerOptionHistoryId",
                table: "OptionChainRaw");

            migrationBuilder.DropIndex(
                name: "IX_OptionChainRaw_TickerOptionHistoryId",
                table: "OptionChainRaw");

            migrationBuilder.DropColumn(
                name: "TickerOptionHistoryId",
                table: "OptionChainRaw");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TickerOptionHistoryId",
                table: "OptionChainRaw",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionChainRaw_TickerOptionHistoryId",
                table: "OptionChainRaw",
                column: "TickerOptionHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionChainRaw_TickerOptionHistories_TickerOptionHistoryId",
                table: "OptionChainRaw",
                column: "TickerOptionHistoryId",
                principalTable: "TickerOptionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
