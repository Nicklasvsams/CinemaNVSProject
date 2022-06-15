using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Transactions
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> SelectAllBookingsAsync();
        Task<Booking> SelectBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> SelectBookingsByShowingIdAsync(int id);
        Task<Booking> DeleteBookingByIdAsync(int id);
        Task<Booking> UpdateBookingByIdAsync(Booking booking, int id);
        Task<Booking> InsertBookingAsync(Booking booking);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly CinemaDBContext _dBContext;

        public BookingRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Booking> DeleteBookingByIdAsync(int id)
        {
            Booking bookingToDelete = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (bookingToDelete != null)
            {
                _dBContext.Remove(bookingToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return bookingToDelete;
        }

        public async Task<Booking> InsertBookingAsync(Booking booking)
        {
            await _dBContext.Bookings.AddAsync(booking);
            await _dBContext.SaveChangesAsync();

            return booking;
        }

        public async Task<IEnumerable<Booking>> SelectAllBookingsAsync()
        {
            return await _dBContext
                .Bookings
                .Include(x => x.Customer)
                .Include(x => x.Showing)
                .Include(x => x.Showing.Movie)
                .Include("BookingSeating.Seating")
                .ToListAsync();
        }

        public async Task<Booking> SelectBookingByIdAsync(int id)
        {
            return await _dBContext
                .Bookings
                .Include(x => x.Customer)
                .Include(x => x.Showing)
                .Include(x => x.Showing.Movie)
                .Include("BookingSeating.Seating")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Booking>> SelectBookingsByShowingIdAsync(int id)
        {
            return await _dBContext
                .Bookings
                .Include(x => x.Customer)
                .Include(x => x.Showing)
                .Include(x => x.Showing.Movie)
                .Include("BookingSeating.Seating")
                .Where(x => x.ShowingId == id)
                .ToListAsync();
        }

        public async Task<Booking> UpdateBookingByIdAsync(Booking booking, int id)
        {
            Booking bookingToUpdate = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (bookingToUpdate != null)
            {
                bookingToUpdate.ShowingId = booking.ShowingId;

                await _dBContext.SaveChangesAsync();
            }

            return bookingToUpdate;
        }
    }
}
