using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Movies
{
    public interface IMovieActorRepository
    {
        public Task<IEnumerable<MovieActor>> SelectAllMovieActorsAsync();
        public Task<MovieActor> InsertMovieActorAsync(MovieActor movieActor);
        public Task<MovieActor> DeleteMovieActorByIdAsync(int movId, int actId);
        public Task<MovieActor> UpdateMovieActorAsync(int movId, int actId, MovieActor movAct);
    }

    public class MovieActorRepository : IMovieActorRepository
    {
        private readonly CinemaDBContext _dBContext;

        public MovieActorRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<MovieActor> DeleteMovieActorByIdAsync(int movId, int actId)
        {
            MovieActor movActToDelete = await _dBContext.MovieActor
                .FirstOrDefaultAsync(x => x.MovieId == movId && x.ActorId == actId);

            if (movActToDelete != null)
            {
                _dBContext.Remove(movActToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return movActToDelete;
        }

        public async Task<IEnumerable<MovieActor>> SelectAllMovieActorsAsync()
        {
            return await _dBContext.MovieActor.ToListAsync();
        }

        public async Task<MovieActor> InsertMovieActorAsync(MovieActor movieActor)
        {
            await _dBContext.MovieActor.AddAsync(movieActor);
            await _dBContext.SaveChangesAsync();

            return movieActor;
        }

        public async Task<MovieActor> UpdateMovieActorAsync(int movId, int actId, MovieActor movAct)
        {
            MovieActor movActToUpdate = await _dBContext.MovieActor
                .FirstOrDefaultAsync(x => x.MovieId == movId && x.ActorId == actId);

            if (movActToUpdate != null)
            {
                movActToUpdate.MovieId = movAct.MovieId;
                movActToUpdate.ActorId = movAct.ActorId;

                await _dBContext.SaveChangesAsync();
            };

            return movActToUpdate;
        }
    }
}
