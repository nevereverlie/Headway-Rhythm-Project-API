using Microsoft.EntityFrameworkCore.Migrations;

namespace Headway_Rhythm_Project_API.Migrations
{
    public partial class GenreOfTheDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGenreOfTheDay",
                table: "Genres",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGenreOfTheDay",
                table: "Genres");
        }
    }
}
