using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly ApplicationDbContext _context;

        public ScreenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Screen>> GetAllAsync()
        {
            return await _context.Screens
                .Include(s => s.Theatre)
                .ToListAsync();
        }

        public async Task<Screen?> GetByIdAsync(int id)
        {
            return await _context.Screens
                .Include(s => s.Theatre)
                .FirstOrDefaultAsync(s => s.ScreenId == id);
        }

        public async Task<IEnumerable<Screen>> GetByTheatreIdAsync(int theatreId)
        {
            return await _context.Screens
                .Include(s => s.Theatre)
                .Where(s => s.TheatreId == theatreId)
                .ToListAsync();
        }

        public async Task<Screen> AddAsync(Screen screen)
        {
            _context.Screens.Add(screen);
            await _context.SaveChangesAsync();
            await _context.Entry(screen).Reference(s => s.Theatre).LoadAsync();
            return screen;
        }

        public async Task UpdateAsync(Screen screen)
        {
            _context.Screens.Update(screen);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Screen screen)
        {
            _context.Screens.Remove(screen);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Screens.AnyAsync(s => s.ScreenId == id);
        }
    }
}