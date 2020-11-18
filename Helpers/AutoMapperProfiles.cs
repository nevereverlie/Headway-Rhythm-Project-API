using AutoMapper;
using Headway_Rhythm_Project_API.Dtos;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserProfileDto>();
            CreateMap<Track, TrackForReturnDto>();
        }
    }
}