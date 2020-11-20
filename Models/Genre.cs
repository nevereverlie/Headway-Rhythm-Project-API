using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Headway_Rhythm_Project_API.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public bool IsGenreOfTheDay { get; set; }
        public ICollection<TrackGenres> TrackGenres { get; set; }
    }
}