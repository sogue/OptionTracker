using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class volDataCall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "PortfolioDate",
                table: "PortfoliosEth",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PortfolioDate",
                table: "PortfoliosEth");
        }
    }
}
