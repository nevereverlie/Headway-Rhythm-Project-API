namespace Headway_Rhythm_Project_API.Models
{
    public class PlaylistTrack
    {
        public int TrackId { get; set; }
        public virtual Track Track { get; set; }

        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}