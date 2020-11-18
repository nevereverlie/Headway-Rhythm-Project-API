using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IGenresRepository
    {
        Task<List<Genre>> GetGenres();
        Task<Genre> GetGenreById(int genreId);
        Task<List<TrackGenres>> GetTrackGenres(string[] genreNames, int trackId);
        Task<List<int>> GetGenresIds(string[] newGenres);
    }
}