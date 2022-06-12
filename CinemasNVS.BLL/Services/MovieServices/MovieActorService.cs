using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.MovieServices
{
    public interface IMovieActorService
    {
        public Task<IEnumerable<MovieActorResponse>> GetAllMovieActorsAsync();
        public Task<MovieActorResponse> CreateMovieActorAsync(MovieActorRequest movActReq);
        public Task<MovieActorResponse> DeleteMovieActorAsync(int id);
    }

    public class MovieActorService : IMovieActorService
    {
        private readonly IMovieActorRepository _movieActorRepository;

        public MovieActorService(IMovieActorRepository movieActorRepository)
        {
            _movieActorRepository = movieActorRepository;
        }

        public async Task<MovieActorResponse> CreateMovieActorAsync(MovieActorRequest movieActor)
        {
            return MapEntityToResponse(await _movieActorRepository.InsertMovieActorAsync(MapRequestToEntity(movieActor)));
        }

        public async Task<IEnumerable<MovieActorResponse>> GetAllMovieActorsAsync()
        {
            IEnumerable<MovieActor> movieActors = await _movieActorRepository.SelectAllMovieActorsAsync();

            return movieActors.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<MovieActorResponse> DeleteMovieActorAsync(int id)
        {
            return MapEntityToResponse(await _movieActorRepository.DeleteMovieActorByIdAsync(id));
        }

        private MovieActor MapRequestToEntity(MovieActorRequest movActRes)
        {
            return new MovieActor()
            {
                MovieId = movActRes.MovieId,
                ActorId = movActRes.ActorId
            };
        }

        private MovieActorResponse MapEntityToResponse(MovieActor movAct)
        {
            return new MovieActorResponse()
            {
                Id = movAct.Id,
                MovieId = movAct.MovieId,
                ActorId = movAct.ActorId
            };
        }
    }
}
