using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.MovieServices
{
    public interface IActorService
    {
        Task<IEnumerable<ActorResponse>> GetAllActorsAsync();
        Task<ActorResponse> GetActorByIdAsync(int actorId);
        Task<ActorResponse> CreateActorAsync(ActorRequest actor);
        Task<ActorResponse> DeleteActorByIdAsync(int actorId);
        Task<ActorResponse> UpdateActorByIdAsync(int actorId, ActorRequest actor);
    }

    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ActorResponse> CreateActorAsync(ActorRequest actor)
        {
            return MapEntityToResponse(await _actorRepository.InsertActorAsync(MapRequestToEntity(actor)));
        }

        public async Task<ActorResponse> DeleteActorByIdAsync(int actorId)
        {
            return MapEntityToResponse(await _actorRepository.DeleteActorByIdAsync(actorId));
        }

        public async Task<ActorResponse> GetActorByIdAsync(int actorId)
        {
            return MapEntityToResponse(await _actorRepository.SelectActorByIdAsync(actorId));
        }

        public async Task<IEnumerable<ActorResponse>> GetAllActorsAsync()
        {
            IEnumerable<Actor> actors = await _actorRepository.SelectAllActorsAsync();

            return actors.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<ActorResponse> UpdateActorByIdAsync(int actorId, ActorRequest actor)
        {
            return MapEntityToResponse(await _actorRepository.UpdateActorByIdAsync(MapRequestToEntity(actor), actorId));
        }

        private ActorResponse MapEntityToResponse(Actor actor)
        {
            ActorResponse actRes = null;

            if (actor != null)
            {
                actRes = new ActorResponse()
                {
                    Id = actor.Id,
                    Name = actor.Name,
                    ImdbLink = actor.ImdbLink
                };

                if(actor.MovieActor != null)
                {
                    List<ActorResponseMovie> movies = new List<ActorResponseMovie>();

                    foreach (var movie in actor.MovieActor)
                    {
                        ActorResponseMovie actorMovieResponse = new ActorResponseMovie()
                        {
                            Id = movie.Movie.Id,
                            Title = movie.Movie.Title,
                            ReleaseDate = movie.Movie.ReleaseDate,
                            ImdbLink = movie.Movie.ImdbLink,
                            RuntimeMinutes = movie.Movie.RuntimeMinutes,
                            TrailerLink = movie.Movie.TrailerLink,
                            DirectorId = movie.Movie.DirectorId
                        };

                        if (movie.Movie.IsRunning == 1) actorMovieResponse.IsRunning = true;
                        else actorMovieResponse.IsRunning = false;

                        movies.Add(actorMovieResponse);
                    }

                    actRes.Movies = movies;
                }
            }

            return actRes;
        }

        private Actor MapRequestToEntity(ActorRequest actReq)
        {
            Actor act = new Actor()
            {
                Name = actReq.Name,
                ImdbLink = actReq.ImdbLink
            };

            return act;
        }
    }
}
