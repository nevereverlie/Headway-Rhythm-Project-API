using Microsoft.EntityFrameworkCore.Migrations;

namespace Headway_Rhythm_Project_API.Migrations
{
    public partial class ExtendedTracksEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackYear",
                table: "Tracks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackYear",
                table: "Tracks");
        }
    }
}
