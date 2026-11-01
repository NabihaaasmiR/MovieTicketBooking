using System;
using System.Collections.Generic;

namespace MovieTicketBooking.DTOs.Bookings
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int ShowId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string TheatreName { get; set; } = string.Empty;
        public string ScreenName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime BookingTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<string> BookedSeatNumbers { get; set; } = new List<string>();
    }
}