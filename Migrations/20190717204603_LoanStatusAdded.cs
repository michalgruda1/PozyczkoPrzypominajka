using Microsoft.EntityFrameworkCore.Migrations;

namespace PozyczkoPrzypominajkaV2.Migrations
{
    public partial class LoanStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Loans",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loans");
        }
    }
}
