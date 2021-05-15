using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class ClassicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "HistoricalOptionContracts",
                newName: "ContractSymbol");

            migrationBuilder.AddColumn<int>(
                name: "HistoicalDateId",
                table: "HistoricalOptionContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HistoricalDateId",
                table: "HistoricalOptionContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricalChain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    ChainSymbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalChain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalChain_Ticker_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Ticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoricalChainId = table.Column<int>(type: "integer", nullable: false),
                    DateSymbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalDate_HistoricalChain_HistoricalChainId",
                        column: x => x.HistoricalChainId,
                        principalTable: "HistoricalChain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalOptionContracts_HistoricalDateId",
                table: "HistoricalOptionContracts",
                column: "HistoricalDateId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalChain_TickerId",
                table: "HistoricalChain",
                column: "TickerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalDate_HistoricalChainId",
                table: "HistoricalDate",
                column: "HistoricalChainId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts",
                column: "HistoricalDateId",
                principalTable: "HistoricalDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.DropTable(
                name: "HistoricalDate");

            migrationBuilder.DropTable(
                name: "HistoricalChain");

            migrationBuilder.DropIndex(
                name: "IX_HistoricalOptionContracts_HistoricalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.DropColumn(
                name: "HistoicalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.DropColumn(
                name: "HistoricalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.RenameColumn(
                name: "ContractSymbol",
                table: "HistoricalOptionContracts",
                newName: "Symbol");
        }
    }
}
