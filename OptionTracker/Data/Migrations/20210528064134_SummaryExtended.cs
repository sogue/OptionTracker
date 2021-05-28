using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class SummaryExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "creation_timestamp",
                table: "PortfoliosEth",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "delta_total",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "futures_pl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "futures_session_rpl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "futures_session_upl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "options_delta",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_gamma",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_pl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_session_rpl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_session_upl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_theta",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_value",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "options_vega",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "portfolio_margining_enabled",
                table: "PortfoliosEth",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "projected_delta_total",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "projected_initial_margin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "projected_maintenance_margin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "session_rpl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "session_upl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_name",
                table: "PortfoliosEth",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "tfa_enabled",
                table: "PortfoliosEth",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "total_pl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "PortfoliosEth",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creation_timestamp",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "delta_total",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "futures_pl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "futures_session_rpl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "futures_session_upl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_delta",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_gamma",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_pl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_session_rpl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_session_upl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_theta",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_value",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "options_vega",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "portfolio_margining_enabled",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "projected_delta_total",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "projected_initial_margin",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "projected_maintenance_margin",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "session_rpl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "session_upl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "system_name",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "tfa_enabled",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "total_pl",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "type",
                table: "PortfoliosEth");
        }
    }
}
