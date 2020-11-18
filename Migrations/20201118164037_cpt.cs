using Microsoft.EntityFrameworkCore.Migrations;

namespace Headway_Rhythm_Project_API.Migrations
{
    public partial class cpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommonPlaylists",
                columns: table => new
                {
                    CommonPlaylistId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommonPlaylistName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonPlaylists", x => x.CommonPlaylistId);
                });

            migrationBuilder.CreateTable(
                name: "CommonPlaylistTracks",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false),
                    CommonPlaylistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonPlaylistTracks", x => new { x.CommonPlaylistId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_CommonPlaylistTracks_CommonPlaylists_CommonPlaylistId",
                        column: x => x.CommonPlaylistId,
                        principalTable: "CommonPlaylists",
                        principalColumn: "CommonPlaylistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonPlaylistTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonPlaylistTracks_TrackId",
                table: "CommonPlaylistTracks",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonPlaylistTracks");

            migrationBuilder.DropTable(
                name: "CommonPlaylists");
        }
    }
}
