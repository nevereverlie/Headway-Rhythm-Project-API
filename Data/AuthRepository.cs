using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(u => u.Username == username))
            {
                return true;
            }

            return false;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if(user == null)
                return null;

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != PasswordHash[i])
                        return false;
                }

                return true;
            }
        }

        public async Task<GoogleUser> RegisterGoogleUser(GoogleUser googleUser)
        {
            await _context.GoogleUsers.AddAsync(googleUser);
            await _context.SaveChangesAsync();

            return googleUser;
        }

        public async Task<bool> GoogleUserExists(string email)
        {
            if(await _context.GoogleUsers.AnyAsync(u => u.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task<GoogleUser> LoginGoogleUser(string email)
        {
            var user = await _context.GoogleUsers.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            return user;
        }
    }
}