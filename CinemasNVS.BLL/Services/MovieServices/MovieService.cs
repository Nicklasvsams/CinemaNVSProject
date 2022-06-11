using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.MovieServices
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponse>> GetAllMoviesAsync();
        Task<MovieResponse> GetMovieByIdAsync(int id);
        Task<MovieResponse> DeleteMovieByIdAsync(int id);
        Task<MovieResponse> UpdateMovieByIdAsync(MovieRequest movie, int id);
        Task<MovieResponse> CreateMovieAsync(MovieRequest movie);
    }

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse> CreateMovieAsync(MovieRequest movie)
        {
            return MapEntityToResponse(await _movieRepository.InsertMovieAsync(MapRequestToEntity(movie)));
        }

        public async Task<MovieResponse> DeleteMovieByIdAsync(int id)
        {
            return MapEntityToResponse(await _movieRepository.DeleteMovieByIdAsync(id));
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMoviesAsync()
        {
            IEnumerable<Movie> movies = await _movieRepository.SelectAllMoviesAsync();

            return movies.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<MovieResponse> GetMovieByIdAsync(int id)
        {
            return MapEntityToResponse(await _movieRepository.SelectMovieByIdAsync(id));
        }

        public async Task<MovieResponse> UpdateMovieByIdAsync(MovieRequest movie, int id)
        {
            return MapEntityToResponse(await _movieRepository.UpdateMovieByIdAsync(MapRequestToEntity(movie), id));
        }

        private MovieResponse MapEntityToResponse(Movie movie)
        {
            MovieResponse movRes = null;

            if (movie != null)
            {
                movRes = new MovieResponse()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    RuntimeMinutes = movie.RuntimeMinutes,
                    TrailerLink = movie.TrailerLink,
                    ImdbLink = movie.ImdbLink,
                    ReleaseDate = movie.ReleaseDate,
                    DirectorId = movie.DirectorId
                };

                if (movie.IsRunning == 1) movRes.IsRunning = true;
                else movRes.IsRunning = false;

                if (movie.MovieActor != null)
                {
                    List<MovieResponseActor> actRes = new List<MovieResponseActor>();

                    foreach (MovieActor actor in movie.MovieActor)
                    {
                        actRes.Add(new MovieResponseActor()
                        {
                            Id = actor.Actor.Id,
                            Name = actor.Actor.Name,
                            ImdbLink = actor.Actor.ImdbLink
                        });

                    }

                    movRes.ActorResponse = actRes;
                }

                if (movie.Director != null)
                {
                    movRes.DirectorResponse = new MovieResponseDirector()
                    {
                        Id = movie.Director.Id,
                        Name = movie.Director.Name,
                        ImdbLink = movie.Director.ImdbLink
                    };
                }

                if (movie.Showings != null)
                {
                    List<MovieResponseShowing> shoRes = new List<MovieResponseShowing>();

                    foreach (Showing showing in movie.Showings)
                    {
                        shoRes.Add(new MovieResponseShowing()
                        {
                            Id = showing.Id,
                            MovieId = showing.MovieId,
                            TimeOfShowing = showing.TimeOfShowing,
                            Price = showing.Price
                        });

                    }
                    movRes.ShowingResponse = shoRes;
                }
            }

            return movRes;
        }

        private Movie MapRequestToEntity(MovieRequest movReq)
        {
            Movie mov = new Movie()
            {
                Title = movReq.Title,
                ReleaseDate = movReq.ReleaseDate,
                ImdbLink = movReq.ImdbLink,
                TrailerLink = movReq.TrailerLink,
                RuntimeMinutes = movReq.RuntimeMinutes,
                DirectorId = movReq.DirectorId
            };

            if (movReq.IsRunning) mov.IsRunning = 1; 
            else mov.IsRunning = 0;

            return mov;
        }
    }
}
