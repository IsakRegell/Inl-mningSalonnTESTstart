using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InlämningSalonn.Models;
using InlämningSalonn.DTOs;
using InlämningSalonn.Interfaces;

namespace InlämningSalonn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SalonBookContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(SalonBookContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        [HttpGet("get-with-query")]
        public async Task<IActionResult> GetCustomerWithquery(
            [FromQuery] string? name,
            [FromQuery] string? sort = "asc",
            [FromQuery] int currentPage = 1,
            [FromQuery] int amountPerPage = 10)

        {
            var result = await _customerService.GetCustomerAsync(name, sort, currentPage, amountPerPage);
            return Ok(result);
        }


        // GET: api/Customer
        [HttpGet("get-all-customers")]
        public async Task<ActionResult<IEnumerable<GetAllCustomersDto>>> GetAllCustomers()
        {
            var customers = await _context.Customers
                .Select(c => new GetAllCustomersDto
                {
                    CustomerId = c.CustomerId,
                    Name = c.Name,
                    Email = c.Email
                })
                .ToListAsync();

            return Ok(customers);
        }
    

    // GET: api/Customer/5
    [HttpGet("get-customer-by/{id}")]
        public async Task<ActionResult<GetAllCustomersDto>> GetCustomerById(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new GetAllCustomersDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email
            };

            return Ok(customerDto);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, SimpleCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = dto.Name;
            customer.Email = dto.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(c => c.CustomerId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-customer")]
        public async Task<ActionResult> CreateCustomer(SimpleCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, dto);
        }

        // DELETE: api/Customer/5
        [HttpDelete("delete-customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
