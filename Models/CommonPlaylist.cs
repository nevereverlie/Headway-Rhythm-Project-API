using System.Collections.Generic;

namespace Headway_Rhythm_Project_API.Models
{
    public class CommonPlaylist
    {
        public int CommonPlaylistId { get; set; }
        public string CommonPlaylistName { get; set; }
        public ICollection<CommonPlaylistTrack> CommonPlaylistTracks { get; set; }
    }
}