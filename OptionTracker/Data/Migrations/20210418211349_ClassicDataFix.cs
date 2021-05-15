using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class ClassicDataFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.DropColumn(
                name: "HistoicalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricalDateId",
                table: "HistoricalOptionContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts",
                column: "HistoricalDateId",
                principalTable: "HistoricalDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricalDateId",
                table: "HistoricalOptionContracts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "HistoicalDateId",
                table: "HistoricalOptionContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricalOptionContracts_HistoricalDate_HistoricalDateId",
                table: "HistoricalOptionContracts",
                column: "HistoricalDateId",
                principalTable: "HistoricalDate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
