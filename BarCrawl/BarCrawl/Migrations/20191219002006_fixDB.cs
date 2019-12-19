using Microsoft.EntityFrameworkCore.Migrations;

namespace BarCrawl.Migrations
{
    public partial class fixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCrawlTable_CrawlTable_CrawlID",
                table: "BarCrawlTable");

            migrationBuilder.AlterColumn<int>(
                name: "CrawlID",
                table: "BarCrawlTable",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BarCrawlTable_CrawlTable_CrawlID",
                table: "BarCrawlTable",
                column: "CrawlID",
                principalTable: "CrawlTable",
                principalColumn: "CrawlID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCrawlTable_CrawlTable_CrawlID",
                table: "BarCrawlTable");

            migrationBuilder.AlterColumn<int>(
                name: "CrawlID",
                table: "BarCrawlTable",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BarCrawlTable_CrawlTable_CrawlID",
                table: "BarCrawlTable",
                column: "CrawlID",
                principalTable: "CrawlTable",
                principalColumn: "CrawlID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
