using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.DTOs.Screens;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenRepository _screenRepository;
        private readonly ITheatreRepository _theatreRepository;

        public ScreenService(IScreenRepository screenRepository, ITheatreRepository theatreRepository)
        {
            _screenRepository = screenRepository;
            _theatreRepository = theatreRepository;
        }

        public async Task<IEnumerable<ScreenResponse>> GetAllScreensAsync()
        {
            var screens = await _screenRepository.GetAllAsync();
            return screens.Select(MapToResponse);
        }

        public async Task<ScreenResponse?> GetScreenByIdAsync(int id)
        {
            var screen = await _screenRepository.GetByIdAsync(id);
            return screen == null ? null : MapToResponse(screen);
        }

        public async Task<IEnumerable<ScreenResponse>> GetScreensByTheatreIdAsync(int theatreId)
        {
            var screens = await _screenRepository.GetByTheatreIdAsync(theatreId);
            return screens.Select(MapToResponse);
        }

        public async Task<ScreenResponse?> CreateScreenAsync(CreateScreenRequest request)
        {
            var theatreExists = await _theatreRepository.ExistsAsync(request.TheatreId);
            if (!theatreExists) return null;

            var screen = new Screen
            {
                TheatreId = request.TheatreId,
                ScreenName = request.ScreenName
            };

            var createdScreen = await _screenRepository.AddAsync(screen);
            return MapToResponse(createdScreen);
        }

        public async Task<bool> UpdateScreenAsync(int id, UpdateScreenRequest request)
        {
            var screen = await _screenRepository.GetByIdAsync(id);
            if (screen == null) return false;

            if (screen.TheatreId != request.TheatreId)
            {
                var theatreExists = await _theatreRepository.ExistsAsync(request.TheatreId);
                if (!theatreExists) return false;
            }

            screen.TheatreId = request.TheatreId;
            screen.ScreenName = request.ScreenName;

            await _screenRepository.UpdateAsync(screen);
            return true;
        }

        public async Task<bool> DeleteScreenAsync(int id)
        {
            var screen = await _screenRepository.GetByIdAsync(id);
            if (screen == null) return false;

            await _screenRepository.DeleteAsync(screen);
            return true;
        }

        private static ScreenResponse MapToResponse(Screen screen)
        {
            return new ScreenResponse
            {
                ScreenId = screen.ScreenId,
                TheatreId = screen.TheatreId,
                TheatreName = screen.Theatre?.Name ?? string.Empty,
                ScreenName = screen.ScreenName
            };
        }
    }
}