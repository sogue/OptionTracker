using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class AddTickerWatchlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PutCall = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExchangeName = table.Column<string>(type: "text", nullable: false),
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
                    Volatility = table.Column<string>(type: "text", nullable: false),
                    Delta = table.Column<string>(type: "text", nullable: false),
                    Gamma = table.Column<string>(type: "text", nullable: false),
                    Theta = table.Column<string>(type: "text", nullable: false),
                    Vega = table.Column<string>(type: "text", nullable: false),
                    Rho = table.Column<string>(type: "text", nullable: false),
                    OpenInterest = table.Column<int>(type: "integer", nullable: false),
                    TimeValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TheoreticalOptionValue = table.Column<string>(type: "text", nullable: false),
                    TheoreticalVolatility = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_OptionContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Watchlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlist", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionContracts");

            migrationBuilder.DropTable(
                name: "Ticker");

            migrationBuilder.DropTable(
                name: "Watchlist");
        }
    }
}
