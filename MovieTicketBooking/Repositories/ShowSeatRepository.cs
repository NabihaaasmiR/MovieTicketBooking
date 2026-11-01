using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class ShowSeatRepository : IShowSeatRepository
    {
        private readonly ApplicationDbContext _context;

        public ShowSeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowSeat>> GetByShowIdAsync(int showId)
        {
            return await _context.ShowSeats
                .Include(ss => ss.Seat)
                .Where(ss => ss.ShowId == showId)
                .ToListAsync();
        }

        public async Task<ShowSeat?> GetByIdAsync(int id)
        {
            return await _context.ShowSeats
                .Include(ss => ss.Seat)
                .FirstOrDefaultAsync(ss => ss.ShowSeatId == id);
        }

        public async Task<IEnumerable<ShowSeat>> GetByIdsAsync(IEnumerable<int> showSeatIds)
        {
            return await _context.ShowSeats
                .Include(ss => ss.Seat)
                .Where(ss => showSeatIds.Contains(ss.ShowSeatId))
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<ShowSeat> showSeats)
        {
            await _context.ShowSeats.AddRangeAsync(showSeats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShowSeat showSeat)
        {
            _context.ShowSeats.Update(showSeat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<ShowSeat> showSeats)
        {
            _context.ShowSeats.UpdateRange(showSeats);
            await _context.SaveChangesAsync();
        }
    }
}