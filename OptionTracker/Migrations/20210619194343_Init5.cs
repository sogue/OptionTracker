using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TickerSector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerSector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TickerType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TickerSymbols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetType = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: false),
                    MarketCap = table.Column<int>(type: "integer", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LastOptionVolume = table.Column<int>(type: "integer", nullable: false),
                    OptionVolumeChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NextEarnings = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TickerSectorId = table.Column<int>(type: "integer", nullable: false),
                    TickerTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerSymbols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TickerSymbols_TickerSector_TickerSectorId",
                        column: x => x.TickerSectorId,
                        principalTable: "TickerSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TickerSymbols_TickerType_TickerTypeId",
                        column: x => x.TickerTypeId,
                        principalTable: "TickerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TickerSymbols_TickerSectorId",
                table: "TickerSymbols",
                column: "TickerSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_TickerSymbols_TickerTypeId",
                table: "TickerSymbols",
                column: "TickerTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickerSymbols");

            migrationBuilder.DropTable(
                name: "TickerSector");

            migrationBuilder.DropTable(
                name: "TickerType");
        }
    }
}
