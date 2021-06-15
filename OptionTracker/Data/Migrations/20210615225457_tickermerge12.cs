using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class tickermerge12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_TickerSector_TickerSectorId",
                table: "Tickers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_TickerType_TickerTypeId",
                table: "Tickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerType",
                table: "TickerType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerSector",
                table: "TickerSector");

            migrationBuilder.RenameTable(
                name: "TickerType",
                newName: "TickerTypes");

            migrationBuilder.RenameTable(
                name: "TickerSector",
                newName: "TickerSectors");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Tickers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerTypes",
                table: "TickerTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerSectors",
                table: "TickerSectors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_TickerSectors_TickerSectorId",
                table: "Tickers",
                column: "TickerSectorId",
                principalTable: "TickerSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_TickerTypes_TickerTypeId",
                table: "Tickers",
                column: "TickerTypeId",
                principalTable: "TickerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_TickerSectors_TickerSectorId",
                table: "Tickers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickers_TickerTypes_TickerTypeId",
                table: "Tickers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerTypes",
                table: "TickerTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TickerSectors",
                table: "TickerSectors");

            migrationBuilder.RenameTable(
                name: "TickerTypes",
                newName: "TickerType");

            migrationBuilder.RenameTable(
                name: "TickerSectors",
                newName: "TickerSector");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickers",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Tickers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickers",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tickers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerType",
                table: "TickerType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TickerSector",
                table: "TickerSector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_TickerSector_TickerSectorId",
                table: "Tickers",
                column: "TickerSectorId",
                principalTable: "TickerSector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickers_TickerType_TickerTypeId",
                table: "Tickers",
                column: "TickerTypeId",
                principalTable: "TickerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
