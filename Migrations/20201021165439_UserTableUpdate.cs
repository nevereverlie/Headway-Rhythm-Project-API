using Microsoft.EntityFrameworkCore.Migrations;

namespace Headway_Rhythm_Project_API.Migrations
{
    public partial class UserTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Users");
        }
    }
}
