using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class deleteUserIdFromLoginModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_UserID",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Logins");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UserID",
                table: "Logins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserID",
                table: "Logins",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_UserID",
                table: "Logins",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
