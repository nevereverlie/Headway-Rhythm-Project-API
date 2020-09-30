using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class TracksRepository : ITracksRepository
    {
        private readonly DataContext _context;
        public TracksRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Track> GetTrackById(int TrackId)
        {
            return await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == TrackId);
        }

        public async Task<List<Track>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }
    }
}