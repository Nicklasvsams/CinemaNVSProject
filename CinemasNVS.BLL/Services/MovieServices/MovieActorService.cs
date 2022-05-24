﻿using CinemaNVS.DAL.Database.Entities.Movies;
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
        public Task<MovieActorResponse> UpdateMovieActorAsync(int movieId, int actorId, MovieActorRequest movActReq);
        public Task<MovieActorResponse> DeleteMovieActorAsync(int movieId, int actorId);
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

        public async Task<MovieActorResponse> UpdateMovieActorAsync(int movieId, int actorId, MovieActorRequest movActReq)
        {
            return MapEntityToResponse(await _movieActorRepository.UpdateMovieActorAsync(movieId, actorId, MapRequestToEntity(movActReq)));
        }

        public async Task<MovieActorResponse> DeleteMovieActorAsync(int movieId, int actorId)
        {
            return MapEntityToResponse(await _movieActorRepository.DeleteMovieActorByIdAsync(movieId, actorId));
        }

        private MovieActor MapRequestToEntity(MovieActorRequest movActRes)
        {
            return new MovieActor()
            {
                MovieId = movActRes.movieId,
                ActorId = movActRes.actorId
            };
        }

        private MovieActorResponse MapEntityToResponse(MovieActor movAct)
        {
            return new MovieActorResponse()
            {
                movieId = movAct.MovieId,
                actorId = movAct.ActorId
            };
        }
    }
}