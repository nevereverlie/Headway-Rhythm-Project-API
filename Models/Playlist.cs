using System.Collections.Generic;

namespace Headway_Rhythm_Project_API.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<UserPlaylist> UserPlaylists { get; set; }
    }
}