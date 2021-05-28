using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class Deribit3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookSummaries_InstrumentId",
                table: "BookSummaries",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries");

            migrationBuilder.DropIndex(
                name: "IX_BookSummaries_InstrumentId",
                table: "BookSummaries");
        }
    }
}
