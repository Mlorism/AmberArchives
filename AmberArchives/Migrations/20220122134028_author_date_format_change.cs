using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmberArchives.Migrations
{
    public partial class author_date_format_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {       migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Authors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
