using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Seats;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/seats")]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeatResponse>>> GetAllSeats([FromQuery] int? screenId)
        {
            if (screenId.HasValue)
            {
                var seatsByScreen = await _seatService.GetSeatsByScreenIdAsync(screenId.Value);
                return Ok(seatsByScreen);
            }

            var seats = await _seatService.GetAllSeatsAsync();
            return Ok(seats);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SeatResponse>> GetSeatById(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);
            if (seat == null) return NotFound();
            return Ok(seat);
        }

        [HttpGet("screen/{screenId:int}")]
        public async Task<ActionResult<IEnumerable<SeatResponse>>> GetSeatsByScreen(int screenId)
        {
            var seats = await _seatService.GetSeatsByScreenIdAsync(screenId);
            return Ok(seats);
        }

        [HttpPost]
        public async Task<ActionResult<SeatResponse>> CreateSeat([FromBody] CreateSeatRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdSeat = await _seatService.CreateSeatAsync(request);
            if (createdSeat == null)
                return BadRequest(new { message = $"Screen with ID {request.ScreenId} does not exist." });

            return CreatedAtAction(nameof(GetSeatById), new { id = createdSeat.SeatId }, createdSeat);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSeat(int id, [FromBody] UpdateSeatRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _seatService.UpdateSeatAsync(id, request);
            if (!success) return NotFound(new { message = "Seat not found or specified Screen ID is invalid." });

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var success = await _seatService.DeleteSeatAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}