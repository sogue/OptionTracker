using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class DeribitNoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries");

            migrationBuilder.DropIndex(
                name: "IX_BookSummaries_InstrumentId",
                table: "BookSummaries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
