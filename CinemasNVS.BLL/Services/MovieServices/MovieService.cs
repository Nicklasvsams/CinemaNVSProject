using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.MovieServices
{
    public interface IMovieService
    {
        public Task<IEnumerable<MovieResponse>> GetAllMovies();
    }

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            IEnumerable<Movie> movies = await _movieRepository.SelectAllMovies();

            return movies.Select(x => MapEntityToResponse(x)).ToList();
        }

        private MovieResponse MapEntityToResponse(Movie movie)
        {
            return new MovieResponse()
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                RuntimeMinutes = movie.RuntimeMinutes,
                IsRunning = movie.IsRunning,
                TrailerLink = movie.TrailerLink,
                ImdbLink = movie.ImdbLink,
                ReleaseDate = movie.ReleaseDate,
                DirectorId = movie.DirectorId
            };
        }
    }
}
