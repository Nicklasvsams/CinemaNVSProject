using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Transactions
{
    public interface IBookingSeatingRepository
    {
        public Task<IEnumerable<BookingSeating>> SelectAllBookingSeatingsAsync();
        public Task<BookingSeating> InsertBookingSeatingAsync(BookingSeating bookingSeating);
        public Task<BookingSeating> DeleteBookingSeatingByIdAsync(int id);
    }

    public class BookingSeatingRepository : IBookingSeatingRepository
    {
        private readonly CinemaDBContext _dBContext;

        public BookingSeatingRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<BookingSeating> DeleteBookingSeatingByIdAsync(int id)
        {
            BookingSeating booSeaToDelete = await _dBContext.BookingSeating
                .FirstOrDefaultAsync(x => x.Id == id);

            if (booSeaToDelete != null)
            {
                _dBContext.Remove(booSeaToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return booSeaToDelete;
        }

        public async Task<BookingSeating> InsertBookingSeatingAsync(BookingSeating bookingSeating)
        {
            await _dBContext.BookingSeating.AddAsync(bookingSeating);
            await _dBContext.SaveChangesAsync();

            return bookingSeating;
        }

        public async Task<IEnumerable<BookingSeating>> SelectAllBookingSeatingsAsync()
        {
            return await _dBContext.BookingSeating.ToListAsync();
        }
    }
}
