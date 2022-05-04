using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Movies
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> SelectAllMovies();
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDBContext _dBContext;

        public MovieRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<Movie>> SelectAllMovies()
        {
            return await _dBContext.Movies.ToListAsync();
        }
    }
}
