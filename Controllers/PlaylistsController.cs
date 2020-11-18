using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Headway_Rhythm_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly ITracksRepository _repo;
        private readonly IAppRepository _apprepo;
        private readonly IPlaylistRepository _playlistrepo;
        private readonly IUserRepository _userrepo;
        public PlaylistsController(ITracksRepository repo,
            IPlaylistRepository playlistrepo,
            IAppRepository apprepo,
            IUserRepository userrepo)
        {
            _repo = repo;
            _playlistrepo = playlistrepo;
            _apprepo = apprepo;
            _userrepo = userrepo;
        }
        [Authorize]
        [HttpPost]
        [Route("add-playlist-for-user/{userId}")]
        public async Task<IActionResult> AddPlaylistForUser([FromBody]Playlist playlist, int userId)
        {
            // return Ok(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _userrepo.GetUserById(userId);

            playlist.User = userFromRepo;
            _apprepo.Add(playlist);
            
            if (await _apprepo.SaveAll()){
                return Ok(playlist);
            }
            return BadRequest("Problem adding playlist");
        }
        [Authorize]
        [HttpGet]
        [Route("get-playlists-of-user/{userId}")]
        public async Task<IActionResult> GetPlaylistsOfUser(int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var playlistsOfUser =  await _playlistrepo.GetPlaylistsOfUser(userId);
            return Ok(playlistsOfUser);
        }
        [Authorize]
        [HttpGet]
        [Route("get-playlist/{playlistId}")]
        public async Task<IActionResult> GetPlaylistOfUser(int playlistId)
        {
            var playlist = await _playlistrepo.GetPlaylist(playlistId);
            return Ok(playlist);
        }

        [Authorize]
        [HttpPost]
        [Route("add-track-to-playlist/{userId}")]
        public async Task<IActionResult> AddTrackToPlaylist([FromBody]PlaylistTrack playlistTrackToAdd, int userId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            _apprepo.Add(playlistTrackToAdd);
            if (await _apprepo.SaveAll()){
                return Ok(playlistTrackToAdd);
            }
            return BadRequest("Problem adding track to playlist");
        }
        [Authorize]
        [HttpGet]
        [Route("get-tracks-of-playlist/{userId}/{playlistId}")]
        public async Task<IActionResult> GetTracksOfPlaylist(int userId, int playlistId)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var tracksOfPlaylist = await _playlistrepo.GetTracksOfPlaylist(playlistId);

            return Ok(tracksOfPlaylist);
        }
    }
}