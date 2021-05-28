using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class SummaryExtended1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "portfolio_margining_enabled",
                table: "PortfoliosEth");

            migrationBuilder.DropColumn(
                name: "tfa_enabled",
                table: "PortfoliosEth");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_vega",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_value",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_theta",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_session_upl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_session_rpl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_pl",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_gamma",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "options_delta",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "MarginBalance",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaintenanceMargin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialMargin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Equity",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "PortfoliosEth",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableWithdrawalFunds",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableFunds",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "options_vega",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_value",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_theta",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_session_upl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_session_rpl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_pl",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_gamma",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "options_delta",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MarginBalance",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaintenanceMargin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialMargin",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Equity",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Currency",
                table: "PortfoliosEth",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableWithdrawalFunds",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableFunds",
                table: "PortfoliosEth",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "portfolio_margining_enabled",
                table: "PortfoliosEth",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "tfa_enabled",
                table: "PortfoliosEth",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
