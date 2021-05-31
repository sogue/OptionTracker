using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class volDataCall8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolumeDatas_VolumeAnals_VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas");

            migrationBuilder.DropIndex(
                name: "IX_VolumeDatas_VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "VolumeDatas");

            migrationBuilder.DropColumn(
                name: "VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "VolumeDatas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDatas_VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas",
                column: "VolumeDataPut_VolumeAnalId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolumeDatas_VolumeAnals_VolumeDataPut_VolumeAnalId",
                table: "VolumeDatas",
                column: "VolumeDataPut_VolumeAnalId",
                principalTable: "VolumeAnals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
