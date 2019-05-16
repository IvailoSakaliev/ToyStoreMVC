using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class ChangeLoginParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Logins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isRegisted",
                table: "Logins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "isRegisted",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}
