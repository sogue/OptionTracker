using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class ComparedChain1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw");

            migrationBuilder.AlterColumn<int>(
                name: "ChainResultViewModelId",
                table: "CompareRaw",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw",
                column: "ChainResultViewModelId",
                principalTable: "ComparedChains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw");

            migrationBuilder.AlterColumn<int>(
                name: "ChainResultViewModelId",
                table: "CompareRaw",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CompareRaw_ComparedChains_ChainResultViewModelId",
                table: "CompareRaw",
                column: "ChainResultViewModelId",
                principalTable: "ComparedChains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
