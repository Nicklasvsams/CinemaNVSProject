using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Movies
{
    public interface IDirectorRepository
    {
        Task<IEnumerable<Director>> SelectAllDirectorsAsync();
        Task<Director> SelectDirectorByIdAsync(int id);
        Task<Director> DeleteDirectorByIdAsync(int id);
        Task<Director> UpdateDirectorByIdAsync(Director director, int id);
        Task<Director> InsertDirectorAsync(Director director);
    }

    public class DirectorRepository : IDirectorRepository
    {
        private readonly CinemaDBContext _dBContext;

        public DirectorRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Director> DeleteDirectorByIdAsync(int id)
        {
            Director directoryToDelete = await _dBContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

            if (directoryToDelete != null)
            {
                _dBContext.Directors.Remove(directoryToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return directoryToDelete;
        }

        public async Task<Director> InsertDirectorAsync(Director director)
        {
            await _dBContext.AddAsync(director);
            await _dBContext.SaveChangesAsync();

            return director;
        }

        public async Task<IEnumerable<Director>> SelectAllDirectorsAsync()
        {
            return await _dBContext.Directors.ToListAsync();
        }

        public async Task<Director> SelectDirectorByIdAsync(int id)
        {
            return await _dBContext.Directors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Director> UpdateDirectorByIdAsync(Director director, int id)
        {
            Director directorToUpdate = await _dBContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

            if (directorToUpdate != null)
            {
                directorToUpdate.Name = director.Name;
                directorToUpdate.ImdbLink = director.ImdbLink;
            }

            return directorToUpdate;
        }
    }
}
