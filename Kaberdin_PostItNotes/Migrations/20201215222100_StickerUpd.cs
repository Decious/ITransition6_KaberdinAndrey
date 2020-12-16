using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaberdin_PostItNotes.Migrations
{
    public partial class StickerUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_WorkColumns_WorkColumnId",
                table: "Stickers");

            migrationBuilder.DropTable(
                name: "WorkColumns");

            migrationBuilder.DropIndex(
                name: "IX_Stickers_WorkColumnId",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "WorkColumnId",
                table: "Stickers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkColumnModelWorkColumnId",
                table: "Stickers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkColumns",
                columns: table => new
                {
                    WorkColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkColumns", x => x.WorkColumnId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_WorkColumnModelWorkColumnId",
                table: "Stickers",
                column: "WorkColumnModelWorkColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_WorkColumns_WorkColumnModelWorkColumnId",
                table: "Stickers",
                column: "WorkColumnModelWorkColumnId",
                principalTable: "WorkColumns",
                principalColumn: "WorkColumnId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
