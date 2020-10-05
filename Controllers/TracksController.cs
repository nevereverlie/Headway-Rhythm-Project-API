using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Data;
using Headway_Rhythm_Project_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITracksRepository _repo;
        public TracksController(ITracksRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns all tracks from database
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTracks()
        {
            var tracksToReturn = await _repo.GetTracks();

            return Ok(tracksToReturn);
        }

        /// <summary>
        /// Returns a specific track from database by Id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTrackById(int id)
        {
            var trackToReturn = await _repo.GetTrackById(id);

            return Ok(trackToReturn);
        }
    }
}