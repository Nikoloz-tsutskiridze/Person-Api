using Microsoft.EntityFrameworkCore;
using Person.Application.Interfaces;
using Person.Core.Domains;
using Person.Infrastructure.Data;

namespace Person.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersWithBirthdayTodayAsync()
        {
            var today = DateTime.Today;

            return await _context.People
                .Where(c => c.DateOfBirth.Day == today.Day && c.DateOfBirth.Month == today.Month)
                .ToListAsync();
        }
    }
}

