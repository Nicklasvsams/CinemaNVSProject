using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Repositories.Transactions;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.TransactionServices
{
    public interface IBookingSeatingService
    {
        public Task<IEnumerable<BookingSeatingResponse>> GetAllBookingSeatingsAsync();
        public Task<BookingSeatingResponse> CreateBookingSeatingAsync(BookingSeatingRequest booSeaReq);
        public Task<BookingSeatingResponse> DeleteBookingSeatingAsync(int id);
    }

    public class BookingSeatingService : IBookingSeatingService
    {
        private readonly IBookingSeatingRepository _bookingSeatingRepository;

        public BookingSeatingService(IBookingSeatingRepository bookingSeatingRepository)
        {
            _bookingSeatingRepository = bookingSeatingRepository;
        }

        public async Task<IEnumerable<BookingSeatingResponse>> GetAllBookingSeatingsAsync()
        {
            IEnumerable<BookingSeating> bookingSeatingResponses = await _bookingSeatingRepository.SelectAllBookingSeatingsAsync();

            return bookingSeatingResponses.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<BookingSeatingResponse> CreateBookingSeatingAsync(BookingSeatingRequest booSeaReq)
        {
            return MapEntityToResponse(await _bookingSeatingRepository.InsertBookingSeatingAsync(MapRequestToEntity(booSeaReq)));
        }

        public async Task<BookingSeatingResponse> DeleteBookingSeatingAsync(int id)
        {
            return MapEntityToResponse(await _bookingSeatingRepository.DeleteBookingSeatingByIdAsync(id));
        }

        private BookingSeating MapRequestToEntity(BookingSeatingRequest booSeaReq)
        {
            return new BookingSeating()
            {
                BookingId = booSeaReq.bookingId,
                SeatingId = booSeaReq.seatingId
            };
        }

        private BookingSeatingResponse MapEntityToResponse(BookingSeating booSea)
        {
            return new BookingSeatingResponse()
            {
                Id = booSea.Id,
                bookingId = booSea.BookingId,
                seatingId = booSea.SeatingId
            };
        }
    }
}
