using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Repositories.Transactions;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.TransactionServices
{
    public interface IShowingService
    {
        Task<IEnumerable<ShowingResponse>> GetAllShowingsAsync();
        Task<ShowingResponse> GetShowingByIdAsync(int id);
        Task<ShowingResponse> DeleteShowingByIdAsync(int id);
        Task<ShowingResponse> UpdateShowingByIdAsync(ShowingRequest showing, int id);
        Task<ShowingResponse> CreateShowingAsync(ShowingRequest showing);
    }

    public class ShowingService : IShowingService
    {
        private readonly IShowingRepository _showingRepository;

        public ShowingService(IShowingRepository showingRepository)
        {
            _showingRepository = showingRepository;
        }

        public async Task<ShowingResponse> CreateShowingAsync(ShowingRequest showing)
        {
            return MapEntityToResponse(await _showingRepository.InsertShowingAsync(MapRequestToEntity(showing)));
        }

        public async Task<ShowingResponse> DeleteShowingByIdAsync(int id)
        {
            return MapEntityToResponse(await _showingRepository.DeleteShowingByIdAsync(id));
        }

        public async Task<IEnumerable<ShowingResponse>> GetAllShowingsAsync()
        {
            IEnumerable<Showing> showings = await _showingRepository.SelectAllShowingAsync();

            return showings.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<ShowingResponse> GetShowingByIdAsync(int id)
        {
            return MapEntityToResponse(await _showingRepository.SelectShowingByIdAsync(id));
        }

        public async Task<ShowingResponse> UpdateShowingByIdAsync(ShowingRequest showing, int id)
        {
            return MapEntityToResponse(await _showingRepository.UpdateShowingByIdAsync(MapRequestToEntity(showing), id));
        }

        private ShowingResponse MapEntityToResponse(Showing showing)
        {
            ShowingResponse showRes = null;

            if (showing != null)
            {
                showRes = new ShowingResponse()
                {
                    Id = showing.Id,
                    Price = showing.Price,
                    TimeOfShowing = showing.TimeOfShowing,
                    MovieId = showing.MovieId
                };

                if (showing.Movie != null)
                {
                    showRes.MovieResponse = new ShowingResponseMovie()
                    {
                        Id = showing.Movie.Id,
                        Title = showing.Movie.Title,
                        ReleaseDate = showing.Movie.ReleaseDate,
                        RuntimeMinutes = showing.Movie.RuntimeMinutes,
                        TrailerLink = showing.Movie.TrailerLink,
                        ImdbLink = showing.Movie.ImdbLink,
                        DirectorId = showing.Movie.DirectorId
                    };

                    if (showing.Movie.IsRunning == 1) showRes.MovieResponse.IsRunning = true;
                    else showRes.MovieResponse.IsRunning = false;
                }

                if (showing.Bookings != null)
                {
                    List<ShowingResponseBooking> booRes = new List<ShowingResponseBooking>();

                    foreach (Booking booking in showing.Bookings)
                    {
                        booRes.Add(new ShowingResponseBooking()
                        {
                            Id = booking.Id,
                            BookingDate = booking.BookingDate,
                            ShowingId = booking.ShowingId,
                            CustomerId = booking.CustomerId
                        });
                    }

                    showRes.BookingResponses = booRes;
                }
            }

            return showRes;
        }

        private Showing MapRequestToEntity(ShowingRequest shoReq)
        {
            Showing sho = new Showing()
            {
                Price = shoReq.Price,
                TimeOfShowing = shoReq.TimeOfShowing,
                MovieId = shoReq.MovieId
            };

            return sho;
        }
    }
}
