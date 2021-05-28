using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OptionTracker.Migrations
{
    public partial class Init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompareRaw",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OpenInterest = table.Column<int>(type: "integer", nullable: false),
                    OpenInterestChange = table.Column<int>(type: "integer", nullable: false),
                    ClosePriceChange = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Volatility = table.Column<decimal>(type: "numeric", nullable: false),
                    VolatilityChange = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompareRaw", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompareRaw");
        }
    }
}
