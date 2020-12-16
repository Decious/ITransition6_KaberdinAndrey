using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaberdin_PostItNotes.Migrations
{
    public partial class Height : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Stickers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Stickers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Stickers");
        }
    }
}
