using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Headway_Rhythm_Project_API.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public ICollection<TrackGenres> Genres { get; set; }

    }
}