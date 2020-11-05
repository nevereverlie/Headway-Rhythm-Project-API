using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Headway_Rhythm_Project_API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 16 characters length.")]
        public string Password { get; set; }
    }
}