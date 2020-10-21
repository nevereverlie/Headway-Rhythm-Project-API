using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Data;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITracksRepository _repo;
        private readonly IAppRepository _apprepo;
        private readonly DataContext _context;
        public TracksController(ITracksRepository repo,
            IAppRepository apprepo, DataContext context)
        {
            _repo = repo;
            _apprepo = apprepo;
            _context = context;
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
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadTrack(IFormFile file, [FromForm]string trackName, [FromForm]string performerName, [FromForm]List<Genre> genres)
        {
            var result = await _repo.AddTrackAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);       

            var trackForCreation = new Track
            {
                TrackName = trackName,
                PerformerName = performerName,
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                DateAdded = System.DateTime.Now.Date
            };

            _apprepo.Add(trackForCreation);
            
            List<TrackGenres> trackGenres = new List<TrackGenres>();
            foreach (Genre genre in genres)
            {
                TrackGenres trackGenresToAdd = new TrackGenres {
                    GenreId = genre.GenreId,
                    TrackId = trackForCreation.TrackId
                };
                trackGenres.Add(trackGenresToAdd);
            }

            trackForCreation.TrackGenres = trackGenres;

            foreach (TrackGenres tGenre in trackForCreation.TrackGenres)
            {
                tGenre.Genre = await _context.Genres.FirstOrDefaultAsync(genre => genre.GenreId == tGenre.GenreId);
            }

            _apprepo.Add(trackForCreation);

            if(await _apprepo.SaveAll()){
                return Ok(trackForCreation);
            }

            return BadRequest("problem adding track");
        }
    }
}