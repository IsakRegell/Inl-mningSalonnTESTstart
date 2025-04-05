using InlämningSalonn.Interfaces;
using InlämningSalonn.Models;
using Microsoft.EntityFrameworkCore;

namespace InlämningSalonn.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly SalonBookContext _context;

        public CustomerService(SalonBookContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomerAsync(string? name, string? sort, int currentPage, int aountPerPahge)
        {
            var query = _context.Customers.AsQueryable(); //AsQueryble = Det gör så att du kan bygga upp din fråga steg för steg

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(customer => customer.Name.Contains(name) || customer.Email.Contains(name));
            }

            query = sort?.ToLower() == "desc"
                ? query.OrderByDescending(customer=> customer.Name)
                : query.OrderBy(customer=> customer.Name);


            query = query.Skip((currentPage - 1) * aountPerPahge).Take(aountPerPahge);

            return await query.ToListAsync();
        }

    }
}
