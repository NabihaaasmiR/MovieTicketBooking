using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieTicketBooking.DTOs.Shows;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IScreenRepository _screenRepository;

        public ShowService(IShowRepository showRepository, IMovieRepository movieRepository, IScreenRepository screenRepository)
        {
            _showRepository = showRepository;
            _movieRepository = movieRepository;
            _screenRepository = screenRepository;
        }

        public async Task<IEnumerable<ShowResponse>> GetAllShowsAsync()
        {
            var shows = await _showRepository.GetAllAsync();
            return shows.Select(MapToResponse);
        }

        public async Task<ShowResponse?> GetShowByIdAsync(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            return show == null ? null : MapToResponse(show);
        }

        public async Task<IEnumerable<ShowResponse>> GetShowsByMovieIdAsync(int movieId)
        {
            var shows = await _showRepository.GetByMovieIdAsync(movieId);
            return shows.Select(MapToResponse);
        }

        public async Task<IEnumerable<ShowResponse>> GetShowsByScreenIdAsync(int screenId)
        {
            var shows = await _showRepository.GetByScreenIdAsync(screenId);
            return shows.Select(MapToResponse);
        }

        public async Task<ShowResponse?> CreateShowAsync(CreateShowRequest request)
        {
            var movieExists = await _movieRepository.ExistsAsync(request.MovieId);
            var screenExists = await _screenRepository.ExistsAsync(request.ScreenId);

            if (!movieExists || !screenExists) return null;

            var show = new Show
            {
                MovieId = request.MovieId,
                ScreenId = request.ScreenId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Price = request.Price
            };

            var createdShow = await _showRepository.AddAsync(show);
            return MapToResponse(createdShow);
        }

        public async Task<bool> UpdateShowAsync(int id, UpdateShowRequest request)
        {
            var show = await _showRepository.GetByIdAsync(id);
            if (show == null) return false;

            if (show.MovieId != request.MovieId)
            {
                var movieExists = await _movieRepository.ExistsAsync(request.MovieId);
                if (!movieExists) return false;
            }

            if (show.ScreenId != request.ScreenId)
            {
                var screenExists = await _screenRepository.ExistsAsync(request.ScreenId);
                if (!screenExists) return false;
            }

            show.MovieId = request.MovieId;
            show.ScreenId = request.ScreenId;
            show.StartTime = request.StartTime;
            show.EndTime = request.EndTime;
            show.Price = request.Price;

            await _showRepository.UpdateAsync(show);
            return true;
        }

        public async Task<bool> DeleteShowAsync(int id)
        {
            var show = await _showRepository.GetByIdAsync(id);
            if (show == null) return false;

            await _showRepository.DeleteAsync(show);
            return true;
        }

        private static ShowResponse MapToResponse(Show show)
        {
            return new ShowResponse
            {
                ShowId = show.ShowId,
                MovieId = show.MovieId,
                MovieTitle = show.Movie?.Title ?? string.Empty,
                ScreenId = show.ScreenId,
                ScreenName = show.Screen?.ScreenName ?? string.Empty,
                TheatreName = show.Screen?.Theatre?.Name ?? string.Empty,
                StartTime = show.StartTime,
                EndTime = show.EndTime,
                Price = show.Price
            };
        }
    }
}