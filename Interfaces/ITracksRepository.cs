using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Http;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface ITracksRepository
    {

        Task<Track> GetTrackById(int TrackId);
        Task<List<Track>> GetTracks();
        Task<VideoUploadResult> AddTrackAsync(IFormFile file);
        Task<DeletionResult> DeleteTrackAsync(string publicId);
    }
}