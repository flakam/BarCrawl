using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCrawl.Migrations
{
    public partial class urladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Bar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Bar");
        }
    }
}
