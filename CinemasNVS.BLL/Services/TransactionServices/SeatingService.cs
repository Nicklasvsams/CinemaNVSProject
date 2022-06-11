using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Repositories.Transactions;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.TransactionServices
{
    public interface ISeatingService
    {
        Task<IEnumerable<SeatingResponse>> GetAllSeatingsAsync();
    }

    public class SeatingService : ISeatingService
    {
        private readonly ISeatingRepository _seatingRepository;

        public SeatingService(ISeatingRepository seatingRepository)
        {
            _seatingRepository = seatingRepository;
        }

        public async Task<IEnumerable<SeatingResponse>> GetAllSeatingsAsync()
        {
            IEnumerable<Seating> seatings = await _seatingRepository.SelectAllSeatingsAsync();

            return seatings.Select(x => MapEntityToResponse(x)).ToList();
        }

        private SeatingResponse MapEntityToResponse(Seating entity)
        {
            return new SeatingResponse()
            {
                Id = entity.Id,
                Seat = entity.Seat
            };
        }
    }
}
