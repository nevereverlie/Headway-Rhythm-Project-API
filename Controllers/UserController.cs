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
        [Route("update")]
        public async Task<IActionResult> UpdateProfile([FromForm]int userId, [FromForm]string Username,
            [FromForm]string Description, IFormFile file)
        {
            var userForUpdate = await _repo.GetUserById(userId);
            if(userForUpdate == null)
                return BadRequest("User does not exist");
            
            if(userForUpdate.Username == Username && userForUpdate.Description == Description && file == null)
                return Ok(userForUpdate);

            userForUpdate.Username = Username;
            userForUpdate.Description = Description;


            // if(file == null) return BadRequest("file = null");
            if(file != null){
                var result = await _repo.AddPhotoAsync(file);

                if(result.Error != null) return BadRequest(result.Error.Message);

                userForUpdate.PhotoUrl = result.SecureUrl.AbsoluteUri;
                userForUpdate.PublicId = result.PublicId;
            }
            
            if(await _apprepo.SaveAll()){
                return Ok(userForUpdate);
            }


            return BadRequest("Problem updating profile");
        }
    }
}