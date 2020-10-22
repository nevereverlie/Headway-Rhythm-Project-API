using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Headway_Rhythm_Project_API.Models;
using Microsoft.AspNetCore.Http;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int TrackId);
        Task<List<User>> GetUsers();
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    }
}