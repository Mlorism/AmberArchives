using Microsoft.EntityFrameworkCore.Migrations;

namespace AmberArchives.Migrations
{
    public partial class create_shelf_and_user_shelf_table_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShelf_Books_BookId",
                table: "UserShelf");

            migrationBuilder.DropIndex(
                name: "IX_UserShelf_BookId",
                table: "UserShelf");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UserShelf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UserShelf",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserShelf_BookId",
                table: "UserShelf",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserShelf_Books_BookId",
                table: "UserShelf",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
