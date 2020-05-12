using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortcutManager.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "Shortcuts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModifierKeys",
                table: "Shortcuts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Shortcuts");

            migrationBuilder.DropColumn(
                name: "ModifierKeys",
                table: "Shortcuts");
        }
    }
}
