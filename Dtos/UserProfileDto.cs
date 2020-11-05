namespace Headway_Rhythm_Project_API.Dtos
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
    }
}