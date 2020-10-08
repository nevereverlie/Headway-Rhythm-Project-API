using System.Collections.Generic;
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

        public Task<DeletionResult> DeleteTrackAsync(string publicId)
        {
            throw new System.NotImplementedException();
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