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
        [Authorize]
        [HttpPost]
        [Route("delete-track-from-playlist/{userId}")]
        public async Task<IActionResult> DeleteTrackFromPlaylist(int userId, [FromBody]PlaylistTrack playlistTrackToDelete)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

             _apprepo.Delete(playlistTrackToDelete);
            if (await _apprepo.SaveAll()){
                return Ok(playlistTrackToDelete);
            }
            return BadRequest("Problem deleting track from playlist");
        }
        [HttpGet]
        [Route("get-common-playlists")]
        public async Task<IActionResult> GetCommonPlaylists()
        {
            var commonPlaylists = await _playlistrepo.GetCommonPlaylists();

            return Ok(commonPlaylists);
        }
        [HttpGet]
        [Route("get-common-playlist/{cpId:int}")]
        public async Task<IActionResult> GetCommonPlaylist(int cpId)
        {
            var commonPlaylist = await _playlistrepo.GetCommonPlaylist(cpId);

            return Ok(commonPlaylist);
        }
        [HttpGet]
        [Route("get-common-playlist-tracks/{cpId:int}")]
        public async Task<IActionResult> GetCommonPlaylistTracks(int cpId)
        {
            var tracks = await _playlistrepo.GetCommonPlaylistTracks(cpId);

            return Ok(tracks);
        }

        [HttpPost]
        [Route("create-common-playlist")]
        public async Task<IActionResult> CreateCommonPlaylist([FromBody]CommonPlaylist commonPlaylist)
        {
            _apprepo.Add(commonPlaylist);

            if (await _apprepo.SaveAll())
                return Ok(commonPlaylist);
            
            return BadRequest("Problem creating common playlist...");
        }

        [HttpPost]
        [Route("add-track-to-common-playlist")]
        public async Task<IActionResult> AddTrackToCommonPlaylist([FromBody]CommonPlaylistTrack commonPlaylistTrack)
        {
            _apprepo.Add(commonPlaylistTrack);

            if (await _apprepo.SaveAll())
                return Ok(200);
            
            return BadRequest("Problem adding track to common playlist...");
        }

        [HttpPut]
        [Route("update-common-playlist/{cpId:int}")]
        public async Task<IActionResult> UpdateCommonPlaylist(int cpId, [FromBody]CommonPlaylist commonPlaylist)
        {
            var cpToUpdate = await _playlistrepo.GetCommonPlaylist(cpId);

            cpToUpdate.CommonPlaylistName = commonPlaylist.CommonPlaylistName;
            cpToUpdate.CommonPlaylistTracks = commonPlaylist.CommonPlaylistTracks;

            _apprepo.Update(cpToUpdate);

            if (await _apprepo.SaveAll())
                return Ok(cpToUpdate);
            
            return BadRequest("Problem updating common playlist...");
        }

        [HttpDelete]
        [Route("delete-common-playlist/{cpId:int}")]
        public async Task<IActionResult> DeleteCommonPlaylist(int cpId)
        {
            var cpToDelete = await _playlistrepo.GetCommonPlaylist(cpId);

            _apprepo.Delete(cpToDelete);

            if (await _apprepo.SaveAll())
                return Ok(200);
            
            return BadRequest("Problem deleting common playlist...");
        }
    }
}