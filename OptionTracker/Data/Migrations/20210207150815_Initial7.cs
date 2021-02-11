using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OptionTracker.Models;
using System;

namespace OptionTracker.Migrations
{
    public partial class Initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionResults_ChainResults_ChainResultId",
                table: "OptionResults");

            migrationBuilder.DropTable(
                name: "ChainResults");

            migrationBuilder.DropTable(
                name: "OptionChain");

            migrationBuilder.DropTable(
                name: "OptionContracts");

            migrationBuilder.DropIndex(
                name: "IX_OptionResults_ChainResultId",
                table: "OptionResults");

            migrationBuilder.DropColumn(
                name: "ChainResultId",
                table: "OptionResults");

            migrationBuilder.CreateTable(
                name: "ChainRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chain = table.Column<Chain>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChainRaw", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChainRaw");

            migrationBuilder.AddColumn<int>(
                name: "ChainResultId",
                table: "OptionResults",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChainResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Ticker = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChainResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionChain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OptionContracts = table.Column<OptionContract[]>(type: "jsonb", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    UnderlyingPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionChain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ask = table.Column<decimal>(type: "numeric", nullable: false),
                    AskSize = table.Column<int>(type: "integer", nullable: false),
                    Bid = table.Column<decimal>(type: "numeric", nullable: false),
                    BidAskSize = table.Column<string>(type: "text", nullable: true),
                    BidSize = table.Column<int>(type: "integer", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    DaysToExpiration = table.Column<int>(type: "integer", nullable: false),
                    DeliverableNote = table.Column<string>(type: "text", nullable: true),
                    Delta = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExchangeName = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationType = table.Column<string>(type: "text", nullable: true),
                    Gamma = table.Column<string>(type: "text", nullable: false),
                    HighPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InTheMoney = table.Column<bool>(type: "boolean", nullable: false),
                    IsIndexOption = table.Column<string>(type: "text", nullable: true),
                    Last = table.Column<decimal>(type: "numeric", nullable: false),
                    LastSize = table.Column<string>(type: "text", nullable: true),
                    LastTradingDay = table.Column<long>(type: "bigint", nullable: false),
                    LowPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Mark = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkChange = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkPercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    Mini = table.Column<bool>(type: "boolean", nullable: false),
                    Multiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    NetChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NonStandard = table.Column<bool>(type: "boolean", nullable: false),
                    OpenInterest = table.Column<int>(type: "integer", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OptionDeliverablesList = table.Column<string>(type: "text", nullable: true),
                    PercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    PutCall = table.Column<string>(type: "text", nullable: false),
                    QuoteTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    Rho = table.Column<string>(type: "text", nullable: false),
                    SettlementType = table.Column<string>(type: "text", nullable: true),
                    StrikePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    TheoreticalOptionValue = table.Column<string>(type: "text", nullable: false),
                    TheoreticalVolatility = table.Column<string>(type: "text", nullable: false),
                    Theta = table.Column<string>(type: "text", nullable: false),
                    TimeValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVolume = table.Column<int>(type: "integer", nullable: false),
                    TradeDate = table.Column<string>(type: "text", nullable: true),
                    TradeTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    Vega = table.Column<string>(type: "text", nullable: false),
                    Volatility = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionContracts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionResults_ChainResultId",
                table: "OptionResults",
                column: "ChainResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionResults_ChainResults_ChainResultId",
                table: "OptionResults",
                column: "ChainResultId",
                principalTable: "ChainResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
