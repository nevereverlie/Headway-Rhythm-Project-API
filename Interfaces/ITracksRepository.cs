using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface ITracksRepository
    {
         Task<Track> GetTrackById(int TrackId);
         Task<List<Track>> GetTracks();

    }
}