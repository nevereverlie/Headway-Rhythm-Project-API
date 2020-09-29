namespace Headway_Rhythm_Project_API.Models
{
    public class TrackGenres
    {
        public int TrackGenresId { get; set; }
        public int TrackId { get; set; }
        public Track Track { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}