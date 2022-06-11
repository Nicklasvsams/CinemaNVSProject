using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Transactions
{
    public interface IShowingRepository
    {
        Task<IEnumerable<Showing>> SelectAllShowingAsync();
        Task<Showing> SelectShowingByIdAsync(int id);
        Task<Showing> DeleteShowingByIdAsync(int id);
        Task<Showing> UpdateShowingByIdAsync(Showing showing, int id);
        Task<Showing> InsertShowingAsync(Showing showing);
    }

    public class ShowingRepository : IShowingRepository
    {
        private readonly CinemaDBContext _dBContext;

        public ShowingRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Showing> DeleteShowingByIdAsync(int id)
        {
            Showing showingToDelete = await _dBContext.Showings.FirstOrDefaultAsync(x => x.Id == id);

            if (showingToDelete != null)
            {
                _dBContext.Remove(showingToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return showingToDelete;
        }

        public async Task<Showing> InsertShowingAsync(Showing showing)
        {
            await _dBContext.Showings.AddAsync(showing);
            await _dBContext.SaveChangesAsync();

            return showing;
        }

        public async Task<IEnumerable<Showing>> SelectAllShowingAsync()
        {
            return await _dBContext
                .Showings
                .Include(x => x.Bookings)
                .Include(x => x.Movie)
                .ToListAsync();
        }

        public async Task<Showing> SelectShowingByIdAsync(int id)
        {
            return await _dBContext
                .Showings
                .Include(x => x.Bookings)
                .Include(x => x.Movie)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Showing> UpdateShowingByIdAsync(Showing showing, int id)
        {
            Showing showingToUpdate = await _dBContext.Showings.FirstOrDefaultAsync(x => x.Id == id);

            if (showingToUpdate != null)
            {
                showingToUpdate.MovieId = showing.MovieId;
                showingToUpdate.Price = showing.Price;
                showingToUpdate.TimeOfShowing = showing.TimeOfShowing;

                await _dBContext.SaveChangesAsync();
            }

            return showingToUpdate;
        }
    }
}
