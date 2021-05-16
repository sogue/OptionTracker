using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class CryptoHist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstrumentHistoryId",
                table: "BookSummaries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstrumentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstrumentName = table.Column<string>(type: "text", nullable: true),
                    ActualInstrumentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentHistories_Instruments_ActualInstrumentId",
                        column: x => x.ActualInstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookSummaries_InstrumentHistoryId",
                table: "BookSummaries",
                column: "InstrumentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentHistories_ActualInstrumentId",
                table: "InstrumentHistories",
                column: "ActualInstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSummaries_InstrumentHistories_InstrumentHistoryId",
                table: "BookSummaries",
                column: "InstrumentHistoryId",
                principalTable: "InstrumentHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSummaries_InstrumentHistories_InstrumentHistoryId",
                table: "BookSummaries");

            migrationBuilder.DropTable(
                name: "InstrumentHistories");

            migrationBuilder.DropIndex(
                name: "IX_BookSummaries_InstrumentHistoryId",
                table: "BookSummaries");

            migrationBuilder.DropColumn(
                name: "InstrumentHistoryId",
                table: "BookSummaries");
        }
    }
}
