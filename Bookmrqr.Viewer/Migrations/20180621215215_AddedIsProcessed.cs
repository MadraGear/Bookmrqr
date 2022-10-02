using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookmrqr.Viewer.Migrations
{
    public partial class AddedIsProcessed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "Bookmarks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "Bookmarks");
        }
    }
}
