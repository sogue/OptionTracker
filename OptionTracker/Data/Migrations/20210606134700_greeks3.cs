using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class greeks3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "instrument_name",
                table: "Stats",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instrument_name",
                table: "Greeks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "instrument_name",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "instrument_name",
                table: "Greeks");
        }
    }
}
