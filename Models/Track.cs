using System.Collections.Generic;

namespace Headway_Rhythm_Project_API.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int Duration { get; set; }
        public ICollection<TrackGenres> Genres { get; set; }
    }
}