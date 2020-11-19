using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Dtos;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> GetPlaylistsOfUser(int id);
        Task<Playlist> GetPlaylist(int playlistId);
        Task<List<Track>> GetTracksOfPlaylist(int playlistId);
        Task<List<CommonPlaylist>> GetCommonPlaylists();
        Task<CommonPlaylist> GetCommonPlaylist(int id);
        Task<List<TrackForReturnDto>> GetCommonPlaylistTracks(int id);
    }
}