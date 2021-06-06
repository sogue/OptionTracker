using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class greeks16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Greeks_greeksId",
                table: "BookDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Stats_statsId",
                table: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookDetails_greeksId",
                table: "BookDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookDetails_statsId",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "greeksId",
                table: "BookDetails");

            migrationBuilder.DropColumn(
                name: "statsId",
                table: "BookDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "greeksId",
                table: "BookDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statsId",
                table: "BookDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_greeksId",
                table: "BookDetails",
                column: "greeksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_statsId",
                table: "BookDetails",
                column: "statsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Greeks_greeksId",
                table: "BookDetails",
                column: "greeksId",
                principalTable: "Greeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Stats_statsId",
                table: "BookDetails",
                column: "statsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
