using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Headway_Rhythm_Project_API.Data;
using Headway_Rhythm_Project_API.Dtos;
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
        private readonly IMapper _mapper;
        private readonly IGenresRepository _genreRepo;
        public TracksController(ITracksRepository repo,
            IGenresRepository genresRepository,
            IAppRepository apprepo,
            IMapper mapper)
        {
            _repo = repo;
            _genreRepo = genresRepository;
            _apprepo = apprepo;
            _mapper = mapper;
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
        public async Task<IActionResult> UploadTrack(IFormFile file, [FromForm]string trackName, [FromForm]string performerName, [FromForm]int year, [FromForm]List<Genre> genres)
        {
            var result = await _repo.AddTrackAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);       

            var trackForCreation = new Track
            {
                TrackName = trackName,
                PerformerName = performerName,
                TrackYear = year,
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
                tGenre.Genre = await _genreRepo.GetGenreById(tGenre.GenreId);
            }

            _apprepo.Add(trackForCreation);

            if(await _apprepo.SaveAll()){
                return Ok(trackForCreation);
            }

            return BadRequest("Problem adding track...");
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateTrack([FromBody]TrackForUpdateDto trackForUpdate)
        {
            var track = await _repo.GetTrackById(trackForUpdate.TrackId);

            var newGenres = trackForUpdate.TrackGenres.Split(",").ToArray();
            for(int i = 0; i < newGenres.Length; i++)
            {
                newGenres[i] = newGenres[i].TrimStart();
            }

            //List<TrackGenres> trackGenres = await _repo.GetTrackGenresById(track.TrackId);
            //List<string> trackGenresNames = await _repo.GetTrackGenresByName(track.TrackId);
            //List<TrackGenres> newTG = await _genreRepo.GetTrackGenres(newGenres, track.TrackId);
            List<int> genresIds = await _genreRepo.GetGenresIds(newGenres);
            List<TrackGenres> trackGenres = new List<TrackGenres>();
            foreach (var genreId in genresIds)
            {
                var tgToAdd = new TrackGenres
                {
                    TrackId = track.TrackId,
                    GenreId = genreId
                };
                trackGenres.Add(tgToAdd);
            }

            track.TrackName = trackForUpdate.TrackName;
            track.PerformerName = trackForUpdate.PerformerName;
            track.TrackYear = trackForUpdate.TrackYear;
            track.TrackGenres.Clear();

            for (int i = 0; i < trackGenres.Count; i++)
            {
                track.TrackGenres.Add(trackGenres[i]);
            }

            _apprepo.Update(track);
            if (await _apprepo.SaveAll())
                return Ok();

            return BadRequest("Problem updating track...");
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var trackToDelete = new Track
            {
                TrackId = id
            };

            _apprepo.Delete(trackToDelete);

            if (await _apprepo.SaveAll())
                return Ok("Track with id " + id + " deleted!");
            
            return BadRequest("Problem deleting this track...");
        }

        [HttpGet]
        [Route("search/{searchString}")]
        public async Task<IActionResult> GetTracksBySearchString(string searchString)
        {
            var tracks = await _repo.GetTracksBySearchString(searchString);
            
            List<Track> tracksToReturn = new List<Track>();
            for (int i = 0; i < tracks.Count; i++)
            {
                tracksToReturn.Add(tracks[i]);
            }
            return Ok(tracksToReturn);
        }
    }
}