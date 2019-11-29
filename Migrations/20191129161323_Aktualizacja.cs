using Microsoft.EntityFrameworkCore.Migrations;

namespace PozyczkoPrzypominajkaV2.Migrations
{
    public partial class Aktualizacja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Notifications",
                newName: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Notifications",
                newName: "Text");
        }
    }
}
