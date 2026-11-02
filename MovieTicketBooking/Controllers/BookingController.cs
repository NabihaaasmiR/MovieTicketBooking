using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.DTOs.ShowSeats;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("show/{showId:int}/seats")]
        public async Task<ActionResult<IEnumerable<ShowSeatResponse>>> GetShowSeats(int showId)
        {
            var seats = await _bookingService.GetShowSeatsAsync(showId);
            return Ok(seats);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] CreateBookingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var booking = await _bookingService.CreateBookingAsync(request);
            if (booking == null)
                return BadRequest(new { message = "Booking failed. Invalid Show ID, or one or more selected seats are already booked." });

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }
    }
}