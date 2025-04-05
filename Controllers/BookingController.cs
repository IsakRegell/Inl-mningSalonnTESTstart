using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InlämningSalonn.Models;
using InlämningSalonn.DTOs;

namespace InlämningSalonn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly SalonBookContext _context;

        public BookingController(SalonBookContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet("get-all-bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Staff)
                .Include(b => b.Service)
                .ToListAsync();

            var result = new List<ShowAllBookingsDto>();

            foreach (var b in bookings)
            {
                var bookingDto = new ShowAllBookingsDto
                {
                    BookingId = b.BookingId,
                    DateTime = b.DateTime,
                    Status = b.Status
                };

                if (b.Customer != null)
                {
                    bookingDto.Customer = new SimpleCustomerDto
                    {
                        Name = b.Customer.Name,
                        Email = b.Customer.Email
                    };
                }

                if (b.Staff != null)
                {
                    bookingDto.Staff = new SimpleStaffDto
                    {

                        Name = b.Staff.Name
                    };
                }

                if (b.Service != null)
                {
                    bookingDto.Service = new SimpleServiceDto
                    { 
                        Name = b.Service.Name
                    };
                }

                result.Add(bookingDto);
            }

            return Ok(result);
        }



        // GET: api/Booking/5
        [HttpGet("get-booking-by-id/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var b = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Staff)
                .Include(b => b.Service)
                .FirstOrDefaultAsync(b => b.BookingId == id);

            if (b == null)
            {
                return NotFound();
            }

            var bookingDto = new ShowAllBookingsDto
            {
                BookingId =b.BookingId,
                Status = b.Status
            };

            if (b.Customer != null)
            {
                bookingDto.Customer = new SimpleCustomerDto
                {
                    Name = b.Customer.Name,
                    Email = b.Customer.Email
                };
            }

            if (b.Staff != null)
            {
                bookingDto.Staff = new SimpleStaffDto
                {
                    Name = b.Staff.Name,
                };
            }

            if (b.Service != null)
            {
                bookingDto.Service = new SimpleServiceDto
                {
                    Name = b.Service.Name,
                };
            }

            return Ok(bookingDto);
        }

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-booking")]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingDto dto)
        {
            var booking = new Booking
            {
                CustomerId = dto.CustomerId,
                StaffId = dto.StaffId,
                ServiceId = dto.ServiceId,
                DateTime = dto.DateTime,
                Status = dto.Status
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingById", new { id = booking.BookingId }, booking);
        }


        // DELETE: api/Booking/5
        [HttpDelete("delete-booking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
