using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class tickermerge11 : Migration
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
                name: "Trader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Tickers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickers_TickerSector_TickerSectorId",
                        column: x => x.TickerSectorId,
                        principalTable: "TickerSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickers_TickerType_TickerTypeId",
                        column: x => x.TickerTypeId,
                        principalTable: "TickerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalChain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    TickerName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalChain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalChain_Tickers_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Tickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TickerTrader1",
                columns: table => new
                {
                    TickersId = table.Column<int>(type: "integer", nullable: false),
                    TradersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TickerTrader1", x => new { x.TickersId, x.TradersId });
                    table.ForeignKey(
                        name: "FK_TickerTrader1_Tickers_TickersId",
                        column: x => x.TickersId,
                        principalTable: "Tickers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TickerTrader1_Trader_TradersId",
                        column: x => x.TradersId,
                        principalTable: "Trader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoricalChainId = table.Column<int>(type: "integer", nullable: false),
                    DateString = table.Column<string>(type: "text", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalDate_HistoricalChain_HistoricalChainId",
                        column: x => x.HistoricalChainId,
                        principalTable: "HistoricalChain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalOptionContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoricalDateId = table.Column<int>(type: "integer", nullable: false),
                    TickerName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalOptionContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalOptionContract_HistoricalDate_HistoricalDateId",
                        column: x => x.HistoricalDateId,
                        principalTable: "HistoricalDate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionContract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoricalOptionContractId = table.Column<int>(type: "integer", nullable: false),
                    PutCall = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExchangeName = table.Column<string>(type: "text", nullable: true),
                    Bid = table.Column<decimal>(type: "numeric", nullable: false),
                    Ask = table.Column<decimal>(type: "numeric", nullable: false),
                    Last = table.Column<decimal>(type: "numeric", nullable: false),
                    Mark = table.Column<decimal>(type: "numeric", nullable: false),
                    BidSize = table.Column<int>(type: "integer", nullable: false),
                    AskSize = table.Column<int>(type: "integer", nullable: false),
                    BidAskSize = table.Column<string>(type: "text", nullable: true),
                    LastSize = table.Column<string>(type: "text", nullable: true),
                    HighPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LowPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalVolume = table.Column<int>(type: "integer", nullable: false),
                    TradeDate = table.Column<string>(type: "text", nullable: true),
                    TradeTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    QuoteTimeInLong = table.Column<long>(type: "bigint", nullable: true),
                    NetChange = table.Column<decimal>(type: "numeric", nullable: false),
                    Volatility = table.Column<string>(type: "text", nullable: true),
                    Delta = table.Column<string>(type: "text", nullable: true),
                    Gamma = table.Column<string>(type: "text", nullable: true),
                    Theta = table.Column<string>(type: "text", nullable: true),
                    Vega = table.Column<string>(type: "text", nullable: true),
                    Rho = table.Column<string>(type: "text", nullable: true),
                    OpenInterest = table.Column<int>(type: "integer", nullable: false),
                    TimeValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TheoreticalOptionValue = table.Column<string>(type: "text", nullable: true),
                    TheoreticalVolatility = table.Column<string>(type: "text", nullable: true),
                    OptionDeliverablesList = table.Column<string>(type: "text", nullable: true),
                    StrikePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationDate = table.Column<long>(type: "bigint", nullable: false),
                    DaysToExpiration = table.Column<int>(type: "integer", nullable: false),
                    ExpirationType = table.Column<string>(type: "text", nullable: true),
                    LastTradingDay = table.Column<long>(type: "bigint", nullable: false),
                    Multiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    SettlementType = table.Column<string>(type: "text", nullable: true),
                    DeliverableNote = table.Column<string>(type: "text", nullable: true),
                    IsIndexOption = table.Column<string>(type: "text", nullable: true),
                    PercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkChange = table.Column<decimal>(type: "numeric", nullable: false),
                    MarkPercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    NonStandard = table.Column<bool>(type: "boolean", nullable: false),
                    Mini = table.Column<bool>(type: "boolean", nullable: false),
                    InTheMoney = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionContract_HistoricalOptionContract_HistoricalOptionCon~",
                        column: x => x.HistoricalOptionContractId,
                        principalTable: "HistoricalOptionContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalChain_TickerId",
                table: "HistoricalChain",
                column: "TickerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalDate_HistoricalChainId",
                table: "HistoricalDate",
                column: "HistoricalChainId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalOptionContract_HistoricalDateId",
                table: "HistoricalOptionContract",
                column: "HistoricalDateId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionContract_HistoricalOptionContractId",
                table: "OptionContract",
                column: "HistoricalOptionContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_TickerSectorId",
                table: "Tickers",
                column: "TickerSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_TickerTypeId",
                table: "Tickers",
                column: "TickerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TickerTrader1_TradersId",
                table: "TickerTrader1",
                column: "TradersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionContract");

            migrationBuilder.DropTable(
                name: "TickerTrader1");

            migrationBuilder.DropTable(
                name: "HistoricalOptionContract");

            migrationBuilder.DropTable(
                name: "Trader");

            migrationBuilder.DropTable(
                name: "HistoricalDate");

            migrationBuilder.DropTable(
                name: "HistoricalChain");

            migrationBuilder.DropTable(
                name: "Tickers");

            migrationBuilder.DropTable(
                name: "TickerSector");

            migrationBuilder.DropTable(
                name: "TickerType");
        }
    }
}
