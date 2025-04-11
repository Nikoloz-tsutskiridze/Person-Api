//public class CustomerRepository : ICustomerRepository
//{
//    private readonly AppDbContext _context;

//    public CustomerRepository(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<List<Customer>> GetCustomersWithBirthdayTodayAsync()
//    {
//        var today = DateTime.Today;
//        return await _context.People
//            .Where(c => c.DateOfBirth.Month == today.Month && c.DateOfBirth.Day == today.Day)
//            .ToListAsync();
//    }

//}
