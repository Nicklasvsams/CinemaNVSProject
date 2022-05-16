using CinemaNVS.DAL.Database.Entities.Movies;
using CinemaNVS.DAL.Repositories.Movies;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.MovieServices
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorResponse>> GetAllDirectorsAsync();
        Task<DirectorResponse> GetDirectorByIdAsync(int directorId);
        Task<DirectorResponse> CreateDirectorAsync(DirectorRequest director);
        Task<DirectorResponse> DeleteDirectorAsync(int directorId);
        Task<DirectorResponse> UpdateDirectorAsync(int directorId, DirectorRequest director);
    }

    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public async Task<DirectorResponse> CreateDirectorAsync(DirectorRequest director)
        {
            return MapEntityToResponse(await _directorRepository.InsertDirectorAsync(MapRequestToEntity(director)));
        }

        public async Task<DirectorResponse> DeleteDirectorAsync(int directorId)
        {
            return MapEntityToResponse(await _directorRepository.DeleteDirectorByIdAsync(directorId));
        }

        public async Task<IEnumerable<DirectorResponse>> GetAllDirectorsAsync()
        {
            IEnumerable<Director> directors = await _directorRepository.SelectAllDirectorsAsync();

            return directors.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<DirectorResponse> GetDirectorByIdAsync(int directorId)
        {
            return MapEntityToResponse(await _directorRepository.SelectDirectorByIdAsync(directorId));
        }

        public async Task<DirectorResponse> UpdateDirectorAsync(int directorId, DirectorRequest director)
        {
            return MapEntityToResponse(await _directorRepository.UpdateDirectorByIdAsync(MapRequestToEntity(director), directorId));
        }

        private DirectorResponse MapEntityToResponse(Director director)
        {
            DirectorResponse dirRes = null;

            if (director != null)
            {
                dirRes = new DirectorResponse()
                {
                    Id = director.Id,
                    Name = director.Name,
                    ImdbLink = director.ImdbLink
                };

                if (director.Movies != null)
                {
                    List<DirectorMovieResponse> movies = new List<DirectorMovieResponse>();

                    foreach (var movie in director.Movies)
                    {
                        DirectorMovieResponse directorMovieResponse = new DirectorMovieResponse()
                        {
                            Id = movie.Id,
                            Title = movie.Title,
                            ReleaseDate = movie.ReleaseDate,
                            ImdbLink = movie.ImdbLink,
                            Rating = movie.Rating,
                            RuntimeMinutes = movie.RuntimeMinutes,
                            TrailerLink = movie.TrailerLink,
                            DirectorId = movie.DirectorId
                        };

                        if (movie.IsRunning == 1) directorMovieResponse.IsRunning = true;
                        else directorMovieResponse.IsRunning = false;

                        movies.Add(directorMovieResponse);
                    }
                }
            }

            return dirRes;
        }

        private Director MapRequestToEntity(DirectorRequest dirReq)
        {
            Director act = new Director()
            {
                Name = dirReq.Name,
                ImdbLink = dirReq.ImdbLink
            };

            return act;
        }
    }
}
