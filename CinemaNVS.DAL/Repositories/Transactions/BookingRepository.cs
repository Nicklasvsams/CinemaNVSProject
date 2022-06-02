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
                .Include("Movie")
                .Include("Customer")
                .ToListAsync();
        }

        public async Task<Booking> SelectBookingByIdAsync(int id)
        {
            return await _dBContext
                .Bookings
                .Include("Movie")
                .Include("Customer")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Booking> UpdateBookingByIdAsync(Booking booking, int id)
        {
            Booking bookingToUpdate = await _dBContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (bookingToUpdate != null)
            {
                bookingToUpdate.Price = booking.Price;
                bookingToUpdate.BookingDate = booking.BookingDate;
                bookingToUpdate.MovieId = booking.MovieId;
                bookingToUpdate.CustomerId = booking.CustomerId;

                await _dBContext.SaveChangesAsync();
            }

            return bookingToUpdate;
        }
    }
}
