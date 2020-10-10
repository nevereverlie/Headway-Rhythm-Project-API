using System;
using System.Collections.Generic;

namespace Headway_Rhythm_Project_API.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string PerformerName { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<TrackGenres> Genres { get; set; } //TRACKGenres
    }
}