using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InlämningSalonn.Models;

namespace InlämningSalonn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SalonBookContext _context;

        public AdminController(SalonBookContext context)
        {
            _context = context;
        }

        // GET: api/Admin
        [HttpGet("get-all-admins")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("get-admin-by/{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-admin/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-admin")]
        public async Task<ActionResult<Admin>> CreateAdmin(Admin admin)
        {
            var existingAdmin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == admin.Email);

            if (existingAdmin != null) // ✅ korrekt null-check
            {
                return Conflict("Emailen används redan.");
            }

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminById", new { id = admin.AdminId }, admin);
        }


        // DELETE: api/Admin/5
        [HttpDelete("delete-admin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
