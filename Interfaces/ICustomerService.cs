using InlämningSalonn.Models;

namespace InlämningSalonn.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomerAsync(string? name, string? sort, int currentPage, int amountPerPage);
                  
    }
}
