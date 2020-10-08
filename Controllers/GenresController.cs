using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Headway_Rhythm_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _repo;
        private readonly IAppRepository _crudRepo;

        public GenresController(IGenresRepository repo, IAppRepository crudRepo)
        {
            _repo = repo;
            _crudRepo = crudRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genresToReturn = await _repo.GetGenres();
            return Ok(genresToReturn);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateGenre(Genre genre)
        {
            var genreToCreate = new Genre
            {
                GenreName = genre.GenreName
            };

            _crudRepo.Add(genreToCreate);

            if (await _crudRepo.SaveAll())
            {
                return Ok(genreToCreate);
            }

            return BadRequest("Problem creating this genre");
        }
        
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteGenre(Genre genre)
        {
            var genreToDelete = new Genre
            {
                GenreId = genre.GenreId
            };

            _crudRepo.Delete(genreToDelete);

            if (await _crudRepo.SaveAll())
            {
                return Ok(genreToDelete);
            }

            return BadRequest("Problem deleting this genre");
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateGenre(Genre genre)
        {
            var genreToUpdate = new Genre
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };

            _crudRepo.Update(genreToUpdate);

            if (await _crudRepo.SaveAll())
            {
                return Ok(genreToUpdate);
            }

            return BadRequest("Problem updating selected genre");
        }
    }
}
