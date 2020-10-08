using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Models;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IGenresRepository
    {
         Task<List<Genre>> GetGenres();
    }
}