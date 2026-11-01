using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly ApplicationDbContext _context;

        public TheatreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Theatre>> GetAllAsync()
        {
            return await _context.Theatres.ToListAsync();
        }

        public async Task<Theatre?> GetByIdAsync(int id)
        {
            return await _context.Theatres.FindAsync(id);
        }

        public async Task<Theatre> AddAsync(Theatre theatre)
        {
            _context.Theatres.Add(theatre);
            await _context.SaveChangesAsync();
            return theatre;
        }

        public async Task UpdateAsync(Theatre theatre)
        {
            _context.Theatres.Update(theatre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Theatre theatre)
        {
            _context.Theatres.Remove(theatre);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Theatres.AnyAsync(t => t.TheatreId == id);
        }
    }
}