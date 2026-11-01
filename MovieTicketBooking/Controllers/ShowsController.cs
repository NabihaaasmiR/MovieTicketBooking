using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Shows;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/shows")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowResponse>>> GetAllShows([FromQuery] int? movieId, [FromQuery] int? screenId)
        {
            if (movieId.HasValue)
            {
                var showsByMovie = await _showService.GetShowsByMovieIdAsync(movieId.Value);
                return Ok(showsByMovie);
            }

            if (screenId.HasValue)
            {
                var showsByScreen = await _showService.GetShowsByScreenIdAsync(screenId.Value);
                return Ok(showsByScreen);
            }

            var shows = await _showService.GetAllShowsAsync();
            return Ok(shows);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShowResponse>> GetShowById(int id)
        {
            var show = await _showService.GetShowByIdAsync(id);
            if (show == null) return NotFound();
            return Ok(show);
        }

        [HttpPost]
        public async Task<ActionResult<ShowResponse>> CreateShow([FromBody] CreateShowRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdShow = await _showService.CreateShowAsync(request);
            if (createdShow == null)
                return BadRequest(new { message = "Invalid Movie ID or Screen ID specified." });

            return CreatedAtAction(nameof(GetShowById), new { id = createdShow.ShowId }, createdShow);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateShow(int id, [FromBody] UpdateShowRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _showService.UpdateShowAsync(id, request);
            if (!success) return NotFound(new { message = "Show not found or specified Movie/Screen ID is invalid." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var success = await _showService.DeleteShowAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}