using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class TickerHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TickerOptionHistoryId",
                table: "OptionChainRaw",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TickerOptionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<int>(type: "integer", nullable: false),
                    TickerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerOptionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TickerOptionHistories_Ticker_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Ticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionChainRaw_TickerOptionHistoryId",
                table: "OptionChainRaw",
                column: "TickerOptionHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TickerOptionHistories_TickerId",
                table: "TickerOptionHistories",
                column: "TickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionChainRaw_TickerOptionHistories_TickerOptionHistoryId",
                table: "OptionChainRaw",
                column: "TickerOptionHistoryId",
                principalTable: "TickerOptionHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionChainRaw_TickerOptionHistories_TickerOptionHistoryId",
                table: "OptionChainRaw");

            migrationBuilder.DropTable(
                name: "TickerOptionHistories");

            migrationBuilder.DropIndex(
                name: "IX_OptionChainRaw_TickerOptionHistoryId",
                table: "OptionChainRaw");

            migrationBuilder.DropColumn(
                name: "TickerOptionHistoryId",
                table: "OptionChainRaw");
        }
    }
}
