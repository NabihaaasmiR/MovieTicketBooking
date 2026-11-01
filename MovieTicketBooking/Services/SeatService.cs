using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.DTOs.Seats;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IScreenRepository _screenRepository;

        public SeatService(ISeatRepository seatRepository, IScreenRepository screenRepository)
        {
            _seatRepository = seatRepository;
            _screenRepository = screenRepository;
        }

        public async Task<IEnumerable<SeatResponse>> GetAllSeatsAsync()
        {
            var seats = await _seatRepository.GetAllAsync();
            return seats.Select(MapToResponse);
        }

        public async Task<SeatResponse?> GetSeatByIdAsync(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            return seat == null ? null : MapToResponse(seat);
        }

        public async Task<IEnumerable<SeatResponse>> GetSeatsByScreenIdAsync(int screenId)
        {
            var seats = await _seatRepository.GetByScreenIdAsync(screenId);
            return seats.Select(MapToResponse);
        }

        public async Task<SeatResponse?> CreateSeatAsync(CreateSeatRequest request)
        {
            var screenExists = await _screenRepository.ExistsAsync(request.ScreenId);
            if (!screenExists) return null;

            var seat = new Seat
            {
                ScreenId = request.ScreenId,
                SeatNumber = request.SeatNumber,
                SeatType = request.SeatType
            };

            var createdSeat = await _seatRepository.AddAsync(seat);
            return MapToResponse(createdSeat);
        }

        public async Task<bool> UpdateSeatAsync(int id, UpdateSeatRequest request)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            if (seat == null) return false;

            if (seat.ScreenId != request.ScreenId)
            {
                var screenExists = await _screenRepository.ExistsAsync(request.ScreenId);
                if (!screenExists) return false;
            }

            seat.ScreenId = request.ScreenId;
            seat.SeatNumber = request.SeatNumber;
            seat.SeatType = request.SeatType;

            await _seatRepository.UpdateAsync(seat);
            return true;
        }

        public async Task<bool> DeleteSeatAsync(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            if (seat == null) return false;

            await _seatRepository.DeleteAsync(seat);
            return true;
        }

        private static SeatResponse MapToResponse(Seat seat)
        {
            return new SeatResponse
            {
                SeatId = seat.SeatId,
                ScreenId = seat.ScreenId,
                ScreenName = seat.Screen?.ScreenName ?? string.Empty,
                SeatNumber = seat.SeatNumber,
                SeatType = seat.SeatType
            };
        }
    }
}