using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Headway_Rhythm_Project_API.Dtos;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PlaylistRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<List<TrackForReturnDto>> GetCommonPlaylistTracks(int id)
        {
            var cpTracks = await (from t in _context.Tracks
                                  join cpt in _context.CommonPlaylistTracks on t.TrackId equals cpt.TrackId
                                  where cpt.CommonPlaylistId == id
                                  select t).ToListAsync();
            List<TrackForReturnDto> tracksForReturn = new List<TrackForReturnDto>();
            foreach (Track track in cpTracks)
            {
                var genresOfTrack = from g in _context.Genres
                                    from tg in _context.TrackGenres
                                    where tg.GenreId == g.GenreId && tg.TrackId == track.TrackId
                                    select g;
                TrackForReturnDto tempDto = _mapper.Map<TrackForReturnDto>(track);
                tempDto.GenresOfTrack = genresOfTrack.ToList();
                tracksForReturn.Add(tempDto);
            }
            return tracksForReturn;

        }
    }
}