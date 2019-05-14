using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class addForeinKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login_id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Orders",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Subject_id",
                table: "Orders",
                newName: "SubjectID");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Logins",
                newName: "UserID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_UserID",
                table: "Logins");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Orders",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "Orders",
                newName: "Subject_id");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Logins",
                newName: "User_id");

            migrationBuilder.AddColumn<int>(
                name: "Login_id",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}
