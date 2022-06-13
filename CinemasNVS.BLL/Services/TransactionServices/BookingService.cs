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
            List<Booking> bookings = (List<Booking>)await _bookingRepository.SelectBookingsByShowingIdAsync(booking.ShowingId);
            List<string> occupiedSeatings = new List<string>();

            foreach (Booking boo in bookings)
            {
                foreach (BookingSeating seat in boo.BookingSeating)
                {
                    occupiedSeatings.Add(seat.Seating.Seat);
                }
            }

            if (occupiedSeatings.Contains(booking.Seat)) return null;
            
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
                    BookingDate = booking.BookingDate,
                    CustomerId = booking.CustomerId,
                    ShowingId = booking.ShowingId
                };

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

                if (booking.Showing != null)
                {
                    BookingResponseShowing shoRes = new BookingResponseShowing()
                    {
                        Id = booking.Showing.Id,
                        MovieId = booking.Showing.MovieId,
                        TimeOfShowing = booking.Showing.TimeOfShowing,
                        Price = booking.Showing.Price
                    };

                    booRes.ShowingResponse = shoRes;

                    if (booking.Showing.Movie != null)
                    {
                        ShowingResponseMovie movRes = new ShowingResponseMovie()
                        {
                            Id = booking.Showing.Movie.Id,
                            Title = booking.Showing.Movie.Title,
                            RuntimeMinutes = booking.Showing.Movie.RuntimeMinutes,
                            ReleaseDate = booking.Showing.Movie.ReleaseDate,
                            TrailerLink = booking.Showing.Movie.TrailerLink,
                            ImdbLink = booking.Showing.Movie.ImdbLink,
                            DirectorId = booking.Showing.Movie.DirectorId
                        };

                        if (booking.Showing.Movie.IsRunning == 1) movRes.IsRunning = true;
                        else movRes.IsRunning = false;

                        booRes.ShowingResponse.MovieResponse = movRes;
                    }
                }

                if (booking.BookingSeating != null)
                {
                    List<BookingResponseSeating> seaRes = new List<BookingResponseSeating>();

                    foreach (BookingSeating booSea in booking.BookingSeating)
                    {
                        seaRes.Add(new BookingResponseSeating()
                        {
                            Id = booSea.Seating.Id,
                            Seat = booSea.Seating.Seat
                        });
                    }

                    booRes.SeatingResponses = seaRes;
                }
            }

            return booRes;
        }

        private Booking MapRequestToEntity(BookingRequest booReq)
        {
            Booking boo = new Booking()
            {
                BookingDate = booReq.BookingDate,
                CustomerId = booReq.CustomerId,
                ShowingId = booReq.ShowingId
            };

            return boo;
        }
    }
}
