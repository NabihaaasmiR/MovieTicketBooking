using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface IShowSeatRepository
    {
        Task<IEnumerable<ShowSeat>> GetByShowIdAsync(int showId);
        Task<ShowSeat?> GetByIdAsync(int id);
        Task<IEnumerable<ShowSeat>> GetByIdsAsync(IEnumerable<int> showSeatIds);
        Task AddRangeAsync(IEnumerable<ShowSeat> showSeats);
        Task UpdateAsync(ShowSeat showSeat);
        Task UpdateRangeAsync(IEnumerable<ShowSeat> showSeats);
    }
}