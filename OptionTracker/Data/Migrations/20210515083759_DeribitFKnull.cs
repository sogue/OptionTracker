using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class DeribitFKnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries");

            migrationBuilder.AlterColumn<int>(
                name: "InstrumentId",
                table: "BookSummaries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries");

            migrationBuilder.AlterColumn<int>(
                name: "InstrumentId",
                table: "BookSummaries",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookSummaries_Instruments_InstrumentId",
                table: "BookSummaries",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
