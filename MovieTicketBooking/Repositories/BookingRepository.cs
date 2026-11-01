using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Show)
                .ThenInclude(s => s.Movie)
                .Include(b => b.Show)
                .ThenInclude(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.ShowSeat)
                .ThenInclude(ss => ss.Seat)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Show)
                .ThenInclude(s => s.Movie)
                .Include(b => b.Show)
                .ThenInclude(s => s.Screen)
                .ThenInclude(sc => sc.Theatre)
                .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.ShowSeat)
                .ThenInclude(ss => ss.Seat)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}