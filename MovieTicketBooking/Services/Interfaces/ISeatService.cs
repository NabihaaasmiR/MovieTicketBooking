using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTicketBooking.DTOs.Seats;

namespace MovieTicketBooking.Services.Interfaces
{
    public interface ISeatService
    {
        Task<IEnumerable<SeatResponse>> GetAllSeatsAsync();
        Task<SeatResponse?> GetSeatByIdAsync(int id);
        Task<IEnumerable<SeatResponse>> GetSeatsByScreenIdAsync(int screenId);
        Task<SeatResponse?> CreateSeatAsync(CreateSeatRequest request);
        Task<bool> UpdateSeatAsync(int id, UpdateSeatRequest request);
        Task<bool> DeleteSeatAsync(int id);
    }
}