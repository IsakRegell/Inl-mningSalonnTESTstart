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
    public class StaffController : ControllerBase
    {
        private readonly SalonBookContext _context;

        public StaffController(SalonBookContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet("get-all-staff")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAllStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        // GET: api/Staff/5
        [HttpGet("get-staff-by/{id}")]
        public async Task<ActionResult<FindStaffById>> GetStaffById(int id)
        {
            var staff = await _context.Staff
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.StaffId == id);

            if (staff == null)
            {
                return NotFound();
            }

            var staffDto = new FindStaffById
            {
                StaffId = staff.StaffId,
                Name = staff.Name,
                Role = staff.Role,
                Bookings = staff.Bookings.Select(b => new BookingDto
                {
                    BookingId = b.BookingId,
                    DateTime = b.DateTime,
                    Status = b.Status
                }).ToList()
            };

            return Ok(staffDto);
        }


        // PUT: api/Staff/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-staff/{id}")]
        public async Task<IActionResult> UpdateStaff(int id, UpdateStaffDto staffToUpdate)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            staff.Name = staffToUpdate.Name;
            staff.Role = staffToUpdate.Role;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // POST: api/Staff
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-staff")]
        public async Task<ActionResult<Staff>> CreateStaff(CreateStaffDto staffToCreate)
        {
            var staff = new Staff
            {
                Name = staffToCreate.Name,
                Role = staffToCreate.Role,
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffById", new { id = staff.StaffId}, staff);
        }



        // DELETE: api/Staff/5
        [HttpDelete("delete-staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }
    }
}
