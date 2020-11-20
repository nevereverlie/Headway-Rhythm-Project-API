using System.Collections.Generic;
using System.Threading.Tasks;
using Headway_Rhythm_Project_API.Interfaces;
using Headway_Rhythm_Project_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<Genre> GetGenreOfTheDay()
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.IsGenreOfTheDay == true);
        }

        public async Task<List<Genre>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<List<int>> GetGenresIds(string[] newGenres)
        {
            var genresIds = new List<int>();

            for (int i = 0; i < newGenres.Length; i++)
            {
                genresIds.AddRange(from g in _context.Genres
                                   where g.GenreName == newGenres[i]
                                   select g.GenreId);
            }

            return genresIds;
        }

        public async Task<List<TrackGenres>> GetTrackGenres(string[] genreNames, int trackId)
        {
            var trackGenres = new List<TrackGenres>();

            for (int i = 0; i < genreNames.Length; i++)
            {
                trackGenres.AddRange(from g in _context.Genres
                                     from t in _context.Tracks
                                     join tg in _context.TrackGenres on new {t.TrackId, g.GenreId} equals new {tg.TrackId, tg.GenreId}
                                     where g.GenreName == genreNames[i] && t.TrackId == trackId
                                     select tg);
            }

            return trackGenres;
        }

        public async Task<Genre> UpdateGenreOfTheDay(int id)
        {
            var genres = await _context.Genres.ToListAsync();
            foreach(Genre g in genres)
            {
                if(g.GenreId == id)
                    g.IsGenreOfTheDay = true;
                else
                    g.IsGenreOfTheDay = false;
            }
            await _context.SaveChangesAsync();
            return await _context.Genres.FirstOrDefaultAsync(g => g.IsGenreOfTheDay == true);
        }
    }
}