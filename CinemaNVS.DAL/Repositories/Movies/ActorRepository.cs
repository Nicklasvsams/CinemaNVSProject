using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Movies
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> SelectAllActorsAsync();
        Task<Actor> SelectActorByIdAsync(int id);
        Task<Actor> DeleteActorByIdAsync(int id);
        Task<Actor> UpdateActorByIdAsync(Actor actor, int id);
        Task<Actor> InsertActorAsync(Actor actor);
    }

    public class ActorRepository : IActorRepository
    {
        private readonly CinemaDBContext _dBContext;

        public ActorRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Actor> DeleteActorByIdAsync(int id)
        {
            Actor actorToDelete = await _dBContext.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actorToDelete != null)
            {
                _dBContext.Actors.Remove(actorToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return actorToDelete;
        }

        public async Task<Actor> InsertActorAsync(Actor actor)
        {
            await _dBContext.Actors.AddAsync(actor);
            await _dBContext.SaveChangesAsync();

            return actor;
        }

        public async Task<Actor> SelectActorByIdAsync(int id)
        {
            return await _dBContext
                .Actors
                .Include("Movies")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Actor>> SelectAllActorsAsync()
        {
            return await _dBContext
                .Actors
                .Include("Movies")
                .ToListAsync();
        }

        public async Task<Actor> UpdateActorByIdAsync(Actor actor, int id)
        {
            Actor actorToUpdate = await _dBContext.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actorToUpdate != null)
            {
                actorToUpdate.Name = actor.Name;
                actorToUpdate.ImdbLink = actor.ImdbLink;

                await _dBContext.SaveChangesAsync();
            }

            return actorToUpdate;
        }
    }
}
