using Microsoft.EntityFrameworkCore.Migrations;

namespace PozyczkoPrzypominajkaV2.Migrations
{
    public partial class UserIDremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
