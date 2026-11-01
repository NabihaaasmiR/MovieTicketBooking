using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories.Interfaces
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> GetAllAsync();
        Task<Show?> GetByIdAsync(int id);
        Task<IEnumerable<Show>> GetByMovieIdAsync(int movieId);
        Task<IEnumerable<Show>> GetByScreenIdAsync(int screenId);
        Task<Show> AddAsync(Show show);
        Task UpdateAsync(Show show);
        Task DeleteAsync(Show show);
        Task<bool> ExistsAsync(int id);
    }
}