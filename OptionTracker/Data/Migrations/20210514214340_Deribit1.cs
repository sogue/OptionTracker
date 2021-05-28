using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Deribit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnderlyingIndex = table.Column<string>(type: "text", nullable: true),
                    Volume = table.Column<decimal>(type: "numeric", nullable: false),
                    VolumeUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    UnderlyingPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BidPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenInterest = table.Column<decimal>(type: "numeric", nullable: false),
                    QuoteCurrency = table.Column<string>(type: "text", nullable: false),
                    High = table.Column<decimal>(type: "numeric", nullable: false),
                    EstimatedDeliveryPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Last = table.Column<decimal>(type: "numeric", nullable: false),
                    MidPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: true),
                    Funding8h = table.Column<decimal>(type: "numeric", nullable: true),
                    MarkPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    AskPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: false),
                    Low = table.Column<decimal>(type: "numeric", nullable: false),
                    BaseCurrency = table.Column<string>(type: "text", nullable: true),
                    CreationTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    CurrentFunding = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuoteCurrency = table.Column<int>(type: "integer", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    TickSize = table.Column<decimal>(type: "numeric", nullable: false),
                    ContractSize = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OptionType = table.Column<int>(type: "integer", nullable: true),
                    MinTradeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InstrumentName = table.Column<string>(type: "text", nullable: false),
                    SettlementPeriod = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<decimal>(type: "numeric", nullable: true),
                    BaseCurrency = table.Column<int>(type: "integer", nullable: false),
                    CreationTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationTimestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSummaries");

            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
