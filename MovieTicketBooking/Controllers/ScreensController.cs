using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Screens;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/screens")]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService _screenService;

        public ScreensController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenResponse>>> GetAllScreens([FromQuery] int? theatreId)
        {
            if (theatreId.HasValue)
            {
                var screensByTheatre = await _screenService.GetScreensByTheatreIdAsync(theatreId.Value);
                return Ok(screensByTheatre);
            }

            var screens = await _screenService.GetAllScreensAsync();
            return Ok(screens);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ScreenResponse>> GetScreenById(int id)
        {
            var screen = await _screenService.GetScreenByIdAsync(id);
            if (screen == null) return NotFound();
            return Ok(screen);
        }

        [HttpGet("theatre/{theatreId:int}")]
        public async Task<ActionResult<IEnumerable<ScreenResponse>>> GetScreensByTheatre(int theatreId)
        {
            var screens = await _screenService.GetScreensByTheatreIdAsync(theatreId);
            return Ok(screens);
        }

        [HttpPost]
        public async Task<ActionResult<ScreenResponse>> CreateScreen([FromBody] CreateScreenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdScreen = await _screenService.CreateScreenAsync(request);
            if (createdScreen == null)
                return BadRequest(new { message = $"Theatre with ID {request.TheatreId} does not exist." });

            return CreatedAtAction(nameof(GetScreenById), new { id = createdScreen.ScreenId }, createdScreen);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateScreen(int id, [FromBody] UpdateScreenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _screenService.UpdateScreenAsync(id, request);
            if (!success) return NotFound(new { message = "Screen not found or specified Theatre ID is invalid." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteScreen(int id)
        {
            var success = await _screenService.DeleteScreenAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}