using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaberdin_PostItNotes.Migrations
{
    public partial class StickerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "DueTime",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Stickers");

            migrationBuilder.AddColumn<int>(
                name: "PositionX",
                table: "Stickers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionY",
                table: "Stickers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "Stickers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Stickers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueTime",
                table: "Stickers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Stickers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
