using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_OptionContracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionContracts");
        }
    }
}
