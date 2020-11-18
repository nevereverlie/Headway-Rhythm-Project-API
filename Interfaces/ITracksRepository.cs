using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Headway_Rhythm_Project_API.Dtos;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Http;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface ITracksRepository
    {

        Task<Track> GetTrackById(int TrackId);
        Task<Track> GetTrackByName(string TrackName);
        // Task<List<Track>> GetTracks();
        Task<List<TrackForReturnDto>> GetTracks();
        Task<VideoUploadResult> AddTrackAsync(IFormFile file);
        Task<List<Track>> GetTracksBySearchString(string searchString);
        Task<bool> UpdateTrack(Track track);
        Task<List<TrackGenres>> GetTrackGenresById(int trackId);
        Task<List<string>> GetTrackGenresByName(int trackId);
    }
}