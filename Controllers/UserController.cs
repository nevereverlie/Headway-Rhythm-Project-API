using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Headway_Rhythm_Project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IAppRepository _apprepo;
        public UserController(IUserRepository repo, IAppRepository apprepo)
        {
            _repo = repo;
            _apprepo = apprepo;
        }

        /// <summary>
        /// Returns all users from database
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersToReturn = await _repo.GetUsers();

            return Ok(usersToReturn);
        }
        /// <summary>
        /// Returns exact user from database
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userToReturn = await _repo.GetUserById(id);

            return Ok(userToReturn);
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadPhoto([FromForm]int userId, IFormFile file)
        {
            var result = await _repo.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var userForPhoto = await _repo.GetUserById(userId);

            userForPhoto.PhotoUrl = result.SecureUrl.AbsoluteUri;
            userForPhoto.PublicId = result.PublicId;
            
            if(await _apprepo.SaveAll()){
                return Ok(userForPhoto);
            }

            return BadRequest("problem adding photo");
        }
    }
}