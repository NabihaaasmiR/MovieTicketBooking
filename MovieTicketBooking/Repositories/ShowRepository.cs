using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationDbContext _context;

        public ShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Show>> GetAllAsync()
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .ToListAsync();
        }

        public async Task<Show?> GetByIdAsync(int id)
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .FirstOrDefaultAsync(s => s.ShowId == id);
        }

        public async Task<IEnumerable<Show>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .Where(s => s.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Show>> GetByScreenIdAsync(int screenId)
        {
            return await _context.Shows
                .Include(s => s.Movie)
                .Include(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .Where(s => s.ScreenId == screenId)
                .ToListAsync();
        }

        public async Task<Show> AddAsync(Show show)
        {
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
            await _context.Entry(show).Reference(s => s.Movie).LoadAsync();
            await _context.Entry(show).Reference(s => s.Screen).LoadAsync();
            if (show.Screen != null)
            {
                await _context.Entry(show.Screen).Reference(sc => sc.Theatre).LoadAsync();
            }
            return show;
        }

        public async Task UpdateAsync(Show show)
        {
            _context.Shows.Update(show);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Show show)
        {
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Shows.AnyAsync(s => s.ShowId == id);
        }
    }
}