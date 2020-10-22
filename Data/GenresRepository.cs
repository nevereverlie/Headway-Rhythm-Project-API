using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Headway_Rhythm_Project_API.Data
{
    public class GenresRepository : IGenresRepository
    {
        private readonly DataContext _context;

        public GenresRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Genre> GetGenreById(int genreId)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == genreId);
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }
    }
}