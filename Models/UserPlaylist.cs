namespace Headway_Rhythm_Project_API.Models
{
    public class UserPlaylist
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PlaylistId { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}