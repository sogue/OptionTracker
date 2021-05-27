using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Ticks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Greeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vega = table.Column<decimal>(type: "numeric", nullable: true),
                    theta = table.Column<decimal>(type: "numeric", nullable: true),
                    rho = table.Column<decimal>(type: "numeric", nullable: true),
                    gamma = table.Column<decimal>(type: "numeric", nullable: true),
                    delta = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Greeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    volume = table.Column<decimal>(type: "numeric", nullable: true),
                    price_change = table.Column<decimal>(type: "numeric", nullable: true),
                    low = table.Column<decimal>(type: "numeric", nullable: true),
                    high = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    underlying_price = table.Column<decimal>(type: "numeric", nullable: true),
                    underlying_index = table.Column<string>(type: "text", nullable: true),
                    timestamp = table.Column<long>(type: "bigint", nullable: false),
                    statsId = table.Column<int>(type: "integer", nullable: true),
                    state = table.Column<string>(type: "text", nullable: true),
                    settlement_price = table.Column<decimal>(type: "numeric", nullable: true),
                    open_interest = table.Column<decimal>(type: "numeric", nullable: true),
                    min_price = table.Column<decimal>(type: "numeric", nullable: true),
                    max_price = table.Column<decimal>(type: "numeric", nullable: true),
                    mark_price = table.Column<decimal>(type: "numeric", nullable: true),
                    mark_iv = table.Column<decimal>(type: "numeric", nullable: true),
                    last_price = table.Column<decimal>(type: "numeric", nullable: true),
                    interest_rate = table.Column<decimal>(type: "numeric", nullable: true),
                    instrument_name = table.Column<string>(type: "text", nullable: true),
                    index_price = table.Column<decimal>(type: "numeric", nullable: true),
                    greeksId = table.Column<int>(type: "integer", nullable: true),
                    estimated_delivery_price = table.Column<decimal>(type: "numeric", nullable: true),
                    bid_iv = table.Column<decimal>(type: "numeric", nullable: true),
                    best_bid_price = table.Column<decimal>(type: "numeric", nullable: true),
                    best_bid_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    best_ask_price = table.Column<decimal>(type: "numeric", nullable: true),
                    best_ask_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    ask_iv = table.Column<decimal>(type: "numeric", nullable: true),
                    InstrumentHistoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookDetails_Greeks_greeksId",
                        column: x => x.greeksId,
                        principalTable: "Greeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookDetails_InstrumentHistories_InstrumentHistoryId",
                        column: x => x.InstrumentHistoryId,
                        principalTable: "InstrumentHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookDetails_Stats_statsId",
                        column: x => x.statsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_greeksId",
                table: "BookDetails",
                column: "greeksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_InstrumentHistoryId",
                table: "BookDetails",
                column: "InstrumentHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetails_statsId",
                table: "BookDetails",
                column: "statsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookDetails");

            migrationBuilder.DropTable(
                name: "Greeks");

            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
