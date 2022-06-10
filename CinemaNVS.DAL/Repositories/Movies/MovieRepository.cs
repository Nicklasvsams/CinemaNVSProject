using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Movies
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> SelectAllMoviesAsync();
        Task<Movie> SelectMovieByIdAsync(int id);
        Task<Movie> DeleteMovieByIdAsync(int id);
        Task<Movie> UpdateMovieByIdAsync(Movie movie, int id);
        Task<Movie> InsertMovieAsync(Movie movie);
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDBContext _dBContext;

        public MovieRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Movie> DeleteMovieByIdAsync(int id)
        {
            Movie movieToDelete = await _dBContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movieToDelete != null)
            {
                _dBContext.Remove(movieToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return movieToDelete;
        }

        public async Task<Movie> InsertMovieAsync(Movie movie)
        {
            await _dBContext.AddAsync(movie);
            await _dBContext.SaveChangesAsync();

            return movie;
        }

        public async Task<IEnumerable<Movie>> SelectAllMoviesAsync()
        {
            return await _dBContext
                .Movies
                .Include("Director")
                .Include("MovieActor.Actor")
                .ToListAsync();
        }

        public async Task<Movie> SelectMovieByIdAsync(int id)
        {
            return await _dBContext
                .Movies
                .Include("Director")
                .Include("MovieActor.Actor")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> UpdateMovieByIdAsync(Movie movie, int id)
        {
            Movie movieToUpdate = await _dBContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Title = movie.Title;
                movieToUpdate.RuntimeMinutes = movie.RuntimeMinutes;
                movieToUpdate.ReleaseDate = movie.ReleaseDate;
                movieToUpdate.IsRunning = movie.IsRunning;
                movieToUpdate.TrailerLink = movie.TrailerLink;
                movieToUpdate.ImdbLink = movie.ImdbLink;
                movieToUpdate.DirectorId = movie.DirectorId;

                await _dBContext.SaveChangesAsync();
            }

            return movieToUpdate;
        }
    }
}
