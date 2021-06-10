using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TickerOptionHistories");

            migrationBuilder.CreateTable(
                name: "OptionActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ticker = table.Column<string>(type: "text", nullable: true),
                    ActivityDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CallVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    PutVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    CallPutRatio = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionActivities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionActivities");

            migrationBuilder.CreateTable(
                name: "TickerOptionHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    TickerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerOptionHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TickerOptionHistories_Ticker_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Ticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TickerOptionHistories_TickerId",
                table: "TickerOptionHistories",
                column: "TickerId");
        }
    }
}
