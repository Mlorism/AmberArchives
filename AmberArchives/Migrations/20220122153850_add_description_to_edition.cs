using Microsoft.EntityFrameworkCore.Migrations;

namespace AmberArchives.Migrations
{
    public partial class add_description_to_edition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Editions");
        }
    }
}
