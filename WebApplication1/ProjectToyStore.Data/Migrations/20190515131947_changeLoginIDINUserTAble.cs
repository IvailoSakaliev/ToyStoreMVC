using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class changeLoginIDINUserTAble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Logins_LoginIDID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LoginIDID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LoginIDID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "LoginID",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "LoginIDID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LoginIDID",
                table: "Users",
                column: "LoginIDID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Logins_LoginIDID",
                table: "Users",
                column: "LoginIDID",
                principalTable: "Logins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
