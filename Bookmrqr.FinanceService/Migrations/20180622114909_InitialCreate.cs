using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookmrqr.FinanceService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessedBookmarks",
                columns: table => new
                {
                    AggregateId = table.Column<string>(nullable: false),
                    IsProcessed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessedBookmarks", x => x.AggregateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessedBookmarks");
        }
    }
}
