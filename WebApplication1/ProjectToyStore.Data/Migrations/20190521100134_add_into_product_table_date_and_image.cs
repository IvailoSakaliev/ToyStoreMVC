using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectToyStore.Data.Migrations
{
    public partial class add_into_product_table_date_and_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Subjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Subjects");
        }
    }
}
