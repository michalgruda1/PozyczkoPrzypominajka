using Microsoft.EntityFrameworkCore.Migrations;

namespace PozyczkoPrzypominajkaV2.Migrations
{
    public partial class LoanFKdefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_GiverId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_ReceiverId",
                table: "Loans");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "986e75b4-92d7-4e90-8516-2e280cec1f74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daca5220-2166-48fb-9acb-32c9eb5180c5");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Loans",
                newName: "ReceiverID");

            migrationBuilder.RenameColumn(
                name: "GiverId",
                table: "Loans",
                newName: "GiverID");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_ReceiverId",
                table: "Loans",
                newName: "IX_Loans_ReceiverID");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_GiverId",
                table: "Loans",
                newName: "IX_Loans_GiverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_GiverID",
                table: "Loans",
                column: "GiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_ReceiverID",
                table: "Loans",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_GiverID",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_ReceiverID",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "ReceiverID",
                table: "Loans",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "GiverID",
                table: "Loans",
                newName: "GiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_ReceiverID",
                table: "Loans",
                newName: "IX_Loans_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_GiverID",
                table: "Loans",
                newName: "IX_Loans_GiverId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "986e75b4-92d7-4e90-8516-2e280cec1f74", "cb7ac25e-da38-4311-8741-a36ae7882bb9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "daca5220-2166-48fb-9acb-32c9eb5180c5", "c667908a-dea9-44be-bd0e-5228e3e5cd8a", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_GiverId",
                table: "Loans",
                column: "GiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_ReceiverId",
                table: "Loans",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
