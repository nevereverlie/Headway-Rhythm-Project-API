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
        public DbSet<TrackGenres> TrackGenres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<UserPlaylist> UserPlaylists { get; set; }
        public DbSet<CommonPlaylist> CommonPlaylists { get; set; }
        public DbSet<CommonPlaylistTrack> CommonPlaylistTracks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaylistTrack>()
                .HasKey(pt => new {pt.PlaylistId, pt.TrackId});
            modelBuilder.Entity<CommonPlaylistTrack>()
                .HasKey(cp => new {cp.CommonPlaylistId, cp.TrackId});
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.PlaylistTracks)
                .WithOne(pt => pt.Playlist)
                .HasForeignKey(pt => pt.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);    
            modelBuilder.Entity<CommonPlaylist>()
                .HasMany(cp => cp.CommonPlaylistTracks)
                .WithOne(cp => cp.CommonPlaylist)
                .HasForeignKey(cp => cp.CommonPlaylistId)
                .OnDelete(DeleteBehavior.Cascade);           
            modelBuilder.Entity<Track>()
                .HasMany(t => t.PlaylistTracks)
                .WithOne(pt => pt.Track)
                .HasForeignKey(pt => pt.TrackId)
                .OnDelete(DeleteBehavior.Cascade);
            

            modelBuilder.Entity<UserPlaylist>()
                .HasKey(up => new {up.PlaylistId, up.UserId});
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserPlaylists)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.UserPlaylists)
                .WithOne(up => up.Playlist)
                .HasForeignKey(up => up.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}