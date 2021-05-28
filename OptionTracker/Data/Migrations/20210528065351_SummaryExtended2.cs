using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class SummaryExtended2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "system_name",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "type",
                table: "PortfoliosEth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "system_name",
                table: "PortfoliosEth",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "PortfoliosEth",
                type: "text",
                nullable: true);
        }
    }
}
