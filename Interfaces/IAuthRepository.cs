using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<User> Login(string username, string password);
        Task<GoogleUser> RegisterGoogleUser(GoogleUser googleUser);
        Task<bool> GoogleUserExists(string email);
        Task<GoogleUser> LoginGoogleUser(string email);
    }
}