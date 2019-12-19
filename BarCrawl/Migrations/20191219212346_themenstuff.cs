using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCrawl.Migrations
{
    public partial class themenstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Bar");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Bar");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "CrawlTable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "CrawlTable");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Bar",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Bar",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
