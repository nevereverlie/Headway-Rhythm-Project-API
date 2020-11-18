namespace Headway_Rhythm_Project_API.Models
{
    public class CommonPlaylistTrack
    {
        public int TrackId { get; set; }
        public Track Track { get; set; }

        public int CommonPlaylistId { get; set; }
        public CommonPlaylist CommonPlaylist { get; set; }
    }
}