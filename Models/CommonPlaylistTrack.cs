namespace Headway_Rhythm_Project_API.Models
{
    public class CommonPlaylistTrack
    {
        public int TrackId { get; set; }
        public virtual Track Track { get; set; }

        public int CommonPlaylistId { get; set; }
        public virtual CommonPlaylist CommonPlaylist { get; set; }
    }
}