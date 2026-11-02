using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        // Accessible by ANY authenticated user (Customer or Admin)
        [HttpPost]
        [Authorize]
        public IActionResult CreateBooking()
        {
            return Ok(new
            {
                message = "Booking created successfully!"
            });
        }

        // Restricted STRICTLY to Admins
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllBookingsForAdmin()
        {
            return Ok(new
            {
                message = "Here are all system bookings (Admin view)."
            });
        }
    }
}
