using System.Collections.Generic;

namespace Headway_Rhythm_Project_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<UserPlaylist> UserPlaylists { get; set; }
    }
}