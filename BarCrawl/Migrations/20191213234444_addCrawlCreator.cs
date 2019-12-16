using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCrawl.Migrations
{
    public partial class addCrawlCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "CrawlTable",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datetime",
                table: "CrawlTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CrawlTable");

            migrationBuilder.DropColumn(
                name: "datetime",
                table: "CrawlTable");
        }
    }
}
