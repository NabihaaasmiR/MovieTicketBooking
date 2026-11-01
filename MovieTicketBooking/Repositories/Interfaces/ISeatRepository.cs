using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetAllAsync();
        Task<Seat?> GetByIdAsync(int id);
        Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId);
        Task<Seat> AddAsync(Seat seat);
        Task AddRangeAsync(IEnumerable<Seat> seats);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(Seat seat);
        Task<bool> ExistsAsync(int id);
    }
}