using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DataContext _context;
        public PlaylistRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Playlist>> GetPlaylistsOfUser(int id)
        {
            var playlistsOfUser = await _context.Playlists.Where(p => p.UserId == id).ToListAsync();
            return playlistsOfUser;
        }

        public async Task<Playlist> GetPlaylist(int playlistId)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(
                p => p.PlaylistId == playlistId);
            return playlist;
        }

        public async Task<List<Track>> GetTracksOfPlaylist(int playlistId)
        {
            var tracksOfPlaylist =
                from t in _context.Tracks
                join pt in _context.PlaylistTracks on t.TrackId equals pt.TrackId
                where pt.PlaylistId == playlistId
                select t;
            return await tracksOfPlaylist.ToListAsync();
        }

        public async Task<List<CommonPlaylist>> GetCommonPlaylists()
        {
            return await _context.CommonPlaylists.ToListAsync();
        }

        public async Task<CommonPlaylist> GetCommonPlaylist(int id)
        {
            return await _context.CommonPlaylists.SingleOrDefaultAsync(cp => cp.CommonPlaylistId == id);
        }

        public async Task<List<Track>> GetCommonPlaylistTracks(int id)
        {
            var cpTracks = from t in _context.Tracks
                           join cpt in _context.CommonPlaylistTracks on t.TrackId equals cpt.TrackId
                           where cpt.CommonPlaylistId == id
                           select t;

            return await cpTracks.ToListAsync();
        }
    }
}