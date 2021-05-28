using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class DailyBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfoliosEth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaintenanceMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableWithdrawalFunds = table.Column<decimal>(type: "numeric", nullable: false),
                    InitialMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableFunds = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<int>(type: "integer", nullable: false),
                    MarginBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Equity = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfoliosEth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BalanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PortfolioEthId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyBalances_PortfoliosEth_PortfolioEthId",
                        column: x => x.PortfolioEthId,
                        principalTable: "PortfoliosEth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    AveragePriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    EstimatedLiquidationPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    FloatingProfitLoss = table.Column<decimal>(type: "numeric", nullable: false),
                    FloatingProfitLossUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    OpenOrdersMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalProfitLoss = table.Column<decimal>(type: "numeric", nullable: false),
                    RealizedProfitLoss = table.Column<decimal>(type: "numeric", nullable: true),
                    Delta = table.Column<decimal>(type: "numeric", nullable: false),
                    InitialMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    MaintenanceMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    MarkPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SettlementPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IndexPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: false),
                    SizeCurrency = table.Column<decimal>(type: "numeric", nullable: true),
                    DailyBalanceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_DailyBalances_DailyBalanceId",
                        column: x => x.DailyBalanceId,
                        principalTable: "DailyBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyBalances_PortfolioEthId",
                table: "DailyBalances",
                column: "PortfolioEthId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DailyBalanceId",
                table: "Positions",
                column: "DailyBalanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "DailyBalances");

            migrationBuilder.DropTable(
                name: "PortfoliosEth");
        }
    }
}
