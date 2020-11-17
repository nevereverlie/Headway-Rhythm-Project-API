using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Headway_Rhythm_Project_API.Helpers;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Headway_Rhythm_Project_API.Data
{
    public class TracksRepository : ITracksRepository
    {
        private readonly DataContext _context;
        private Cloudinary _cloudinary;
        public TracksRepository(DataContext context, IOptions<CloudinarySettings> cloudinaryConfig)
        {
             _context = context;

            Account acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }
        public async Task<VideoUploadResult> AddTrackAsync(IFormFile file)
        {
            var uploadResult = new VideoUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new VideoUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult;

        }

        public async Task<Track> GetTrackById(int TrackId)
        {
            return await _context.Tracks.FirstOrDefaultAsync(t => t.TrackId == TrackId);
        }
        public async Task<Track> GetTrackByName(string TrackName)
        {
            return await _context.Tracks.FirstOrDefaultAsync(t => t.TrackName == TrackName);
        }

        public async Task<List<Track>> GetTracks()
        {
            return await _context.Tracks.Include(tg => tg.TrackGenres).ToListAsync();
        }

        public async Task<List<TrackGenres>> GetTrackGenresById(int trackId)
        {
            var trackGenres = from t in _context.Tracks
                              from g in _context.Genres
                              join tg in _context.TrackGenres on new {t.TrackId, g.GenreId} equals new {tg.TrackId, tg.GenreId}
                              where tg.TrackId == trackId && tg.GenreId == g.GenreId
                              select tg;
                              
            return await trackGenres.ToListAsync();
        }
        public async Task<List<string>> GetTrackGenresByName(int trackId)
        {
            var trackGenres = from t in _context.Tracks
                              join tg in _context.TrackGenres on t.TrackId equals tg.TrackId
                              where tg.TrackId == trackId
                              join g in _context.Genres on tg.GenreId equals g.GenreId
                              select g.GenreName;
                              
            return await trackGenres.ToListAsync();
        }

        public async Task<List<Track>> GetTracksBySearchString(string searchString)
        {
            var tracks = await _context.Tracks.Where(t => t.PerformerName.Contains(searchString) || 
                t.TrackName.Contains(searchString)).ToListAsync();
            
            return tracks;
        }

        public Task<bool> UpdateTrack(Track track)
        {
            throw new System.NotImplementedException();
        }
    }
}