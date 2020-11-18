namespace Headway_Rhythm_Project_API.Dtos
{
    public class TrackForUpdateDto
    {
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string PerformerName { get; set; }
        public int TrackYear { get; set; }
        public string TrackGenres { get; set; }
    }
}