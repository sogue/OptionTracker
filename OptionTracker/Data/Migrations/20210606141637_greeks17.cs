using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class greeks17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "volume",
                table: "Stats",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "low",
                table: "Stats",
                newName: "Low");

            migrationBuilder.RenameColumn(
                name: "high",
                table: "Stats",
                newName: "High");

            migrationBuilder.RenameColumn(
                name: "price_change",
                table: "Stats",
                newName: "PriceChange");

            migrationBuilder.RenameColumn(
                name: "instrument_name",
                table: "Stats",
                newName: "InstrumentName");

            migrationBuilder.RenameColumn(
                name: "vega",
                table: "Greeks",
                newName: "Vega");

            migrationBuilder.RenameColumn(
                name: "theta",
                table: "Greeks",
                newName: "Theta");

            migrationBuilder.RenameColumn(
                name: "rho",
                table: "Greeks",
                newName: "Rho");

            migrationBuilder.RenameColumn(
                name: "gamma",
                table: "Greeks",
                newName: "Gamma");

            migrationBuilder.RenameColumn(
                name: "delta",
                table: "Greeks",
                newName: "Delta");

            migrationBuilder.RenameColumn(
                name: "instrument_name",
                table: "Greeks",
                newName: "InstrumentName");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "BookDetails",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "BookDetails",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "underlying_price",
                table: "BookDetails",
                newName: "UnderlyingPrice");

            migrationBuilder.RenameColumn(
                name: "underlying_index",
                table: "BookDetails",
                newName: "UnderlyingIndex");

            migrationBuilder.RenameColumn(
                name: "settlement_price",
                table: "BookDetails",
                newName: "SettlementPrice");

            migrationBuilder.RenameColumn(
                name: "open_interest",
                table: "BookDetails",
                newName: "OpenInterest");

            migrationBuilder.RenameColumn(
                name: "min_price",
                table: "BookDetails",
                newName: "MinPrice");

            migrationBuilder.RenameColumn(
                name: "max_price",
                table: "BookDetails",
                newName: "MaxPrice");

            migrationBuilder.RenameColumn(
                name: "mark_price",
                table: "BookDetails",
                newName: "MarkPrice");

            migrationBuilder.RenameColumn(
                name: "mark_iv",
                table: "BookDetails",
                newName: "MarkIv");

            migrationBuilder.RenameColumn(
                name: "last_price",
                table: "BookDetails",
                newName: "LastPrice");

            migrationBuilder.RenameColumn(
                name: "interest_rate",
                table: "BookDetails",
                newName: "InterestRate");

            migrationBuilder.RenameColumn(
                name: "instrument_name",
                table: "BookDetails",
                newName: "InstrumentName");

            migrationBuilder.RenameColumn(
                name: "index_price",
                table: "BookDetails",
                newName: "IndexPrice");

            migrationBuilder.RenameColumn(
                name: "estimated_delivery_price",
                table: "BookDetails",
                newName: "EstimatedDeliveryPrice");

            migrationBuilder.RenameColumn(
                name: "bid_iv",
                table: "BookDetails",
                newName: "BidIv");

            migrationBuilder.RenameColumn(
                name: "best_bid_price",
                table: "BookDetails",
                newName: "BestBidPrice");

            migrationBuilder.RenameColumn(
                name: "best_bid_amount",
                table: "BookDetails",
                newName: "BestBidAmount");

            migrationBuilder.RenameColumn(
                name: "best_ask_price",
                table: "BookDetails",
                newName: "BestAskPrice");

            migrationBuilder.RenameColumn(
                name: "best_ask_amount",
                table: "BookDetails",
                newName: "BestAskAmount");

            migrationBuilder.RenameColumn(
                name: "ask_iv",
                table: "BookDetails",
                newName: "AskIv");

            migrationBuilder.AddColumn<int>(
                name: "BookDetailId",
                table: "Stats",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookDetailId",
                table: "Greeks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_BookDetailId",
                table: "Stats",
                column: "BookDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Greeks_BookDetailId",
                table: "Greeks",
                column: "BookDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Greeks_BookDetails_BookDetailId",
                table: "Greeks",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_BookDetails_BookDetailId",
                table: "Stats",
                column: "BookDetailId",
                principalTable: "BookDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greeks_BookDetails_BookDetailId",
                table: "Greeks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_BookDetails_BookDetailId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_BookDetailId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Greeks_BookDetailId",
                table: "Greeks");

            migrationBuilder.DropColumn(
                name: "BookDetailId",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "BookDetailId",
                table: "Greeks");

            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "Stats",
                newName: "volume");

            migrationBuilder.RenameColumn(
                name: "Low",
                table: "Stats",
                newName: "low");

            migrationBuilder.RenameColumn(
                name: "High",
                table: "Stats",
                newName: "high");

            migrationBuilder.RenameColumn(
                name: "PriceChange",
                table: "Stats",
                newName: "price_change");

            migrationBuilder.RenameColumn(
                name: "InstrumentName",
                table: "Stats",
                newName: "instrument_name");

            migrationBuilder.RenameColumn(
                name: "Vega",
                table: "Greeks",
                newName: "vega");

            migrationBuilder.RenameColumn(
                name: "Theta",
                table: "Greeks",
                newName: "theta");

            migrationBuilder.RenameColumn(
                name: "Rho",
                table: "Greeks",
                newName: "rho");

            migrationBuilder.RenameColumn(
                name: "Gamma",
                table: "Greeks",
                newName: "gamma");

            migrationBuilder.RenameColumn(
                name: "Delta",
                table: "Greeks",
                newName: "delta");

            migrationBuilder.RenameColumn(
                name: "InstrumentName",
                table: "Greeks",
                newName: "instrument_name");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "BookDetails",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "BookDetails",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "UnderlyingPrice",
                table: "BookDetails",
                newName: "underlying_price");

            migrationBuilder.RenameColumn(
                name: "UnderlyingIndex",
                table: "BookDetails",
                newName: "underlying_index");

            migrationBuilder.RenameColumn(
                name: "SettlementPrice",
                table: "BookDetails",
                newName: "settlement_price");

            migrationBuilder.RenameColumn(
                name: "OpenInterest",
                table: "BookDetails",
                newName: "open_interest");

            migrationBuilder.RenameColumn(
                name: "MinPrice",
                table: "BookDetails",
                newName: "min_price");

            migrationBuilder.RenameColumn(
                name: "MaxPrice",
                table: "BookDetails",
                newName: "max_price");

            migrationBuilder.RenameColumn(
                name: "MarkPrice",
                table: "BookDetails",
                newName: "mark_price");

            migrationBuilder.RenameColumn(
                name: "MarkIv",
                table: "BookDetails",
                newName: "mark_iv");

            migrationBuilder.RenameColumn(
                name: "LastPrice",
                table: "BookDetails",
                newName: "last_price");

            migrationBuilder.RenameColumn(
                name: "InterestRate",
                table: "BookDetails",
                newName: "interest_rate");

            migrationBuilder.RenameColumn(
                name: "InstrumentName",
                table: "BookDetails",
                newName: "instrument_name");

            migrationBuilder.RenameColumn(
                name: "IndexPrice",
                table: "BookDetails",
                newName: "index_price");

            migrationBuilder.RenameColumn(
                name: "EstimatedDeliveryPrice",
                table: "BookDetails",
                newName: "estimated_delivery_price");

            migrationBuilder.RenameColumn(
                name: "BidIv",
                table: "BookDetails",
                newName: "bid_iv");

            migrationBuilder.RenameColumn(
                name: "BestBidPrice",
                table: "BookDetails",
                newName: "best_bid_price");

            migrationBuilder.RenameColumn(
                name: "BestBidAmount",
                table: "BookDetails",
                newName: "best_bid_amount");

            migrationBuilder.RenameColumn(
                name: "BestAskPrice",
                table: "BookDetails",
                newName: "best_ask_price");

            migrationBuilder.RenameColumn(
                name: "BestAskAmount",
                table: "BookDetails",
                newName: "best_ask_amount");

            migrationBuilder.RenameColumn(
                name: "AskIv",
                table: "BookDetails",
                newName: "ask_iv");
        }
    }
}
