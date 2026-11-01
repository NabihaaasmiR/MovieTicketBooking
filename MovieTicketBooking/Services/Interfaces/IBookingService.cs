using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.DTOs.ShowSeats;

namespace MovieTicketBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();
        Task<BookingResponse?> GetBookingByIdAsync(int id);
        Task<BookingResponse?> CreateBookingAsync(CreateBookingRequest request);
        Task<IEnumerable<ShowSeatResponse>> GetShowSeatsAsync(int showId);
    }
}