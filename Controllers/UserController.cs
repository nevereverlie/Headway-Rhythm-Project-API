using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Headway_Rhythm_Project_API.Dtos;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
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

        private readonly IMapper _mapper;
        public UserController(IUserRepository repo,
                              IAppRepository apprepo,
                              IMapper mapper)
        {
            _repo = repo;
            _apprepo = apprepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all users from database
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserProfileDto>>(users);

            return Ok(usersToReturn);
        }
        /// <summary>
        /// Returns exact user from database
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.GetUserById(id);

            var userToReturn = _mapper.Map<UserProfileDto>(user);

            return Ok(userToReturn);
        }
        [HttpPut]
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

            if(file != null){
                var result = await _repo.AddPhotoAsync(file);

                if(result.Error != null) return BadRequest(result.Error.Message);

                userForUpdate.PhotoUrl = result.SecureUrl.AbsoluteUri;
                userForUpdate.PublicId = result.PublicId;
            }
            
            if(await _apprepo.SaveAll()){
                var userToReturn = _mapper.Map<UserProfileDto>(userForUpdate);
                return Ok(userToReturn);
            }

            return BadRequest("Problem updating profile");
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var userToDelete = new User
            {
                UserId = id
            };

            _apprepo.Delete(userToDelete);

            if (await _apprepo.SaveAll())
                return Ok(userToDelete);

            return BadRequest("Problem deleting this user");
        }
    }
}