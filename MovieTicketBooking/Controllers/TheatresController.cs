using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Theatres;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/theatres")]
    public class TheatresController : ControllerBase
    {
        private readonly ITheatreService _theatreService;

        public TheatresController(ITheatreService theatreService)
        {
            _theatreService = theatreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheatreResponse>>> GetAllTheatres()
        {
            var theatres = await _theatreService.GetAllTheatresAsync();
            return Ok(theatres);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TheatreResponse>> GetTheatreById(int id)
        {
            var theatre = await _theatreService.GetTheatreByIdAsync(id);
            if (theatre == null) return NotFound();
            return Ok(theatre);
        }

        [HttpPost]
        public async Task<ActionResult<TheatreResponse>> CreateTheatre([FromBody] CreateTheatreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdTheatre = await _theatreService.CreateTheatreAsync(request);
            return CreatedAtAction(nameof(GetTheatreById), new { id = createdTheatre.TheatreId }, createdTheatre);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTheatre(int id, [FromBody] UpdateTheatreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _theatreService.UpdateTheatreAsync(id, request);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTheatre(int id)
        {
            var success = await _theatreService.DeleteTheatreAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}