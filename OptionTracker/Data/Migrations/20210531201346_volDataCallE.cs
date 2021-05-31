using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class volDataCallE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionType",
                table: "VolumeDatas",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionType",
                table: "VolumeDatas");
        }
    }
}
