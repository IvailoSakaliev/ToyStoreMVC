using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class add_property_name_in_basemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TypeSubjects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "HashString",
                table: "Hashs",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Logins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TypeSubjects",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hashs",
                newName: "HashString");
        }
    }
}
