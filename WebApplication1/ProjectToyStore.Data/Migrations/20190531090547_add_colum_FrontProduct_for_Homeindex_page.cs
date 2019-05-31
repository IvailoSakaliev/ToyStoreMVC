using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class add_colum_FrontProduct_for_Homeindex_page : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Front",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Front",
                table: "Subjects");
        }
    }
}
