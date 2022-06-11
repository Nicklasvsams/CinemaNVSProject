using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Transactions
{
    public interface ISeatingRepository
    {
        Task<IEnumerable<Seating>> SelectAllSeatingsAsync();
    }
    public class SeatingRepository : ISeatingRepository
    {
        private readonly CinemaDBContext _dBContext;

        public SeatingRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<Seating>> SelectAllSeatingsAsync()
        {
            return await _dBContext.Seatings.ToListAsync();
        }
    }
}
