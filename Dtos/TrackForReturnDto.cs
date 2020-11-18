using System;
using System.Collections.Generic;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Dtos
{
    public class TrackForReturnDto
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string PerformerName { get; set; }
        public int TrackYear { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<TrackGenres> TrackGenres { get; set; }
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public List<Genre> GenresOfTrack { get; set; }
    }
}