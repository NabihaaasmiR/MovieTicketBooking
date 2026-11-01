using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await _context.Seats
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(int id)
        {
            return await _context.Seats
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .FirstOrDefaultAsync(s => s.SeatId == id);
        }

        public async Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId)
        {
            return await _context.Seats
                .Include(s => s.Screen)
                .Where(s => s.ScreenId == screenId)
                .ToListAsync();
        }

        public async Task<Seat> AddAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
            await _context.Entry(seat).Reference(s => s.Screen).LoadAsync();
            return seat;
        }

        public async Task AddRangeAsync(IEnumerable<Seat> seats)
        {
            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seat seat)
        {
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Seats.AnyAsync(s => s.SeatId == id);
        }
    }
}