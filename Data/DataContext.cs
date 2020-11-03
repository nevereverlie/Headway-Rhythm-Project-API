using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Track> Tracks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GoogleUser> GoogleUsers { get; set; }

    }
}