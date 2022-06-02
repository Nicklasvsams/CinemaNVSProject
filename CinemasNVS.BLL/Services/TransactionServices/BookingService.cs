using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Repositories.Transactions;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.TransactionServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();
        Task<BookingResponse> GetBookingByIdAsync(int id);
        Task<BookingResponse> DeleteBookingByIdAsync(int id);
        Task<BookingResponse> UpdateBookingByIdAsync(BookingRequest booking, int id);
        Task<BookingResponse> CreateBookingAsync(BookingRequest booking);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> DeleteBookingByIdAsync(int id)
        {
            return MapEntityToResponse(await _bookingRepository.DeleteBookingByIdAsync(id));
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingRequest booking)
        {
            return MapEntityToResponse(await _bookingRepository.InsertBookingAsync(MapRequestToEntity(booking)));
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
        {
            IEnumerable<Booking> bookings = await _bookingRepository.SelectAllBookingsAsync();

            return bookings.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<BookingResponse> GetBookingByIdAsync(int id)
        {
            return MapEntityToResponse(await _bookingRepository.SelectBookingByIdAsync(id));
        }

        public async Task<BookingResponse> UpdateBookingByIdAsync(BookingRequest booking, int id)
        {
            return MapEntityToResponse(await _bookingRepository.UpdateBookingByIdAsync(MapRequestToEntity(booking), id));
        }

        private BookingResponse MapEntityToResponse(Booking booking)
        {
            BookingResponse booRes = null;

            if (booking != null)
            {
                booRes = new BookingResponse()
                {
                    Id = booking.Id,
                    Price = booking.Price,
                    BookingDate = booking.BookingDate,
                    MovieId = booking.MovieId,
                    CustomerId = booking.CustomerId
                };

                if (booking.Movie != null)
                {
                    booRes.MovieResponse = new BookingResponseMovie()
                    {
                        Id = booking.Movie.Id,
                        Title = booking.Movie.Title,
                        Rating = booking.Movie.Rating,
                        ReleaseDate = booking.Movie.ReleaseDate,
                        RuntimeMinutes = booking.Movie.RuntimeMinutes,
                        ImdbLink = booking.Movie.ImdbLink,
                        TrailerLink = booking.Movie.TrailerLink
                    };

                    if (booking.Movie.IsRunning == 1) booRes.MovieResponse.IsRunning = true;
                    else booRes.MovieResponse.IsRunning = false;
                }

                if (booking.Customer != null)
                {
                    booRes.CustomerResponse = new BookingResponseCustomer()
                    {
                        Id = booking.Customer.Id,
                        FirstName = booking.Customer.FirstName,
                        LastName = booking.Customer.LastName,
                        Email = booking.Customer.Email,
                        PhoneNo = booking.Customer.PhoneNo
                    };

                    if (booking.Customer.IsActive == "yes") booRes.CustomerResponse.IsActive = true;
                    else booRes.CustomerResponse.IsActive = false;
                }
            }

            return booRes;
        }

        private Booking MapRequestToEntity(BookingRequest booReq)
        {
            Booking boo = new Booking()
            {
                Price = booReq.Price,
                BookingDate = booReq.BookingDate,
                MovieId = booReq.MovieId,
                CustomerId = booReq.CustomerId
            };

            return boo;
        }
    }
}
